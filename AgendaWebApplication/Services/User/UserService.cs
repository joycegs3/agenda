using AgendaWebApplication.Data;
using AgendaWebApplication.Dto.User;
using AgendaWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaWebApplication.Services.User
{
    public class UserService : IUserInterface
    {

        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserModel> CreateUser(UserModel user)
        {
            UserModel response = new UserModel();

            try
            {
                var newUser = new UserModel()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber
                };

                // Verificar se o e-mail já está cadastrado
                var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == newUser.Email);

                if (existingUser != null)
                {
                    throw new Exception("E-mail já cadastrado!");
                }

                _context.User.Add(newUser);
                await _context.SaveChangesAsync();

                response = newUser;

                return response;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task<ResponseModel<UserModel>> DeleteUser(int idUser)
        {
            ResponseModel<UserModel> response = new ResponseModel<UserModel>();

            try
            {
                var user = await _context.User.FirstOrDefaultAsync(u => u.Id == idUser);

                if (user == null)
                {
                    response.Message = "Esse usuário não existe!";

                    return response;
                }

                _context.User.Remove(user);
                await _context.SaveChangesAsync();

                response.Message = "Usuário removido com sucesso!";

                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<UserModel> GetUserByEmailAndPassword(UserModel user)
        {
            UserModel response = new UserModel();

            try
            {
                var emailAndPass = await _context.User.
                    FirstOrDefaultAsync(userDatabase => userDatabase.Email.Equals(user.Email) && userDatabase.Password.Equals(user.Password));

                if (emailAndPass == null)
                {
                    return response;
                }

                response = emailAndPass;

                return response;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseModel<UserModel>> GetCurentUser(int userId)
        {
            ResponseModel<UserModel> response = new ResponseModel<UserModel>();

            try
            {
                var user = await _context.User.
                    FirstOrDefaultAsync(userDatabase => userDatabase.Id == userId);

                if (user == null)
                {
                    response.Message = "Usuário não encontrado!";

                    return response;
                }

                response.Data = user;
                response.Message = "Usuário encontrado!";

                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<List<UserModel>>> ListUsers()
        {
            ResponseModel<List<UserModel>> response = new ResponseModel<List<UserModel>>();

            try
            {
                var users = await _context.User.ToListAsync();
                response.Data = users;
                response.Message = "Usuários coletados com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;

                return response;

            }
        }
    }
}
