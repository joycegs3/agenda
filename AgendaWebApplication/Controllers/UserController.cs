using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AgendaWebApplication.Dto.Agenda;
using AgendaWebApplication.Dto.User;
using AgendaWebApplication.Models;
using AgendaWebApplication.Services.Agenda;
using AgendaWebApplication.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AgendaWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userInterface;

        private IConfiguration _configuration;

        public UserController(IUserInterface userInterface, IConfiguration configuration)
        {
            _userInterface = userInterface;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserModel>> Register([FromBody] UserModel model)
        {
            var user = await _userInterface.CreateUser(model);
            return Ok(user);
        }

        [HttpGet("Login/{email}/{password}")]
        public async Task<ActionResult> Login(string email, string password)
        {
            UserModel model = new UserModel()
            {
                Email = email,
                Password = password
            };

            var user = await UserAuthentication(model);

            user.Token = GenerateToken(model);

            return Ok(user);
        }

        [HttpGet("GetCurentUser/{userId}")]
        public async Task<ActionResult<ResponseModel<UserModel>>> GetCurentUser(int userId)
        {
            var user = await _userInterface.GetCurentUser(userId);
            return Ok(user);
        }

        [HttpGet("ListUsers")]
        public async Task<ActionResult<ResponseModel<List<AgendaModel>>>> ListUsers()
        {
            var users = await _userInterface.ListUsers();

            return Ok(users);
        }

        //[HttpGet("GetUserByEmailAndPassword/{username, passoword}")]
        //public async Task<ActionResult<ResponseModel<UserModel>>> GetUserByEmailAndPassword(string username, string password)
        //{
        //    var user = await _userInterface.GetUserByEmailAndPassword(username, password);
        //    return Ok(user);
        //}

        //[HttpDelete("DeleteUser")]
        //public async Task<ActionResult<ResponseModel<UserModel>>> DeleteUser(int userId)
        //{
        //    var user = await _userInterface.DeleteUser(userId);
        //    return Ok(user);
        //}

        private string GenerateToken(UserModel model)
        {

            var key = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("A chave JWT (Jwt:Key) não pode ser nula ou vazia.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private async Task<UserModel> UserAuthentication(UserModel user)
        {
            return await _userInterface.GetUserByEmailAndPassword(user);
        }


    }
}
