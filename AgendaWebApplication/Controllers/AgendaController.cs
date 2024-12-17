using AgendaWebApplication.Services.Agenda;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AgendaWebApplication.Models;
using Microsoft.VisualBasic;
using AgendaWebApplication.Dto.Agenda;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace AgendaWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaInterface _agendaInterface;
        private IConfiguration _configuration;

        public AgendaController(IAgendaInterface agendaInterface, IConfiguration configuration)
        {
            _agendaInterface = agendaInterface;
            _configuration = configuration;
        }

        [HttpGet("ListInformations")] 
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ResponseModel<List<AgendaModel>>>> ListInformations()
        {

            var informations = await _agendaInterface.ListInformations();

            return Ok(informations);
        }

        [HttpGet("ListById/{idInformation}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ResponseModel<AgendaModel>>> ListById(int idInformation)
        {
            var information = await _agendaInterface.GetInformationById(idInformation);
            return Ok(information);
        }

        [HttpGet("ListByPhoneNumber/{phoneNumber}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ResponseModel<AgendaModel>>> ListByPhoneNumber(string phoneNumber)
        {
            var information = await _agendaInterface.GetInformationByPhoneNumber(phoneNumber);
            return Ok(information);
        }

        [HttpPost("CreateInformation")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ResponseModel<List<AgendaModel>>>> CreateInformation(AgendaCreationDto agendaCreationDto)
        {
            var informations = await _agendaInterface.CreateInformation(agendaCreationDto);
            return Ok(informations);
        }

        [HttpPut("EditInformation")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ResponseModel<List<AgendaModel>>>> EditInformation(AgendaUpdateDto agendaUpdateDto)
        {
            var informations = await _agendaInterface.EditInformation(agendaUpdateDto);
            return Ok(informations);
        }

        [HttpPut("EditInformationByEmail")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ResponseModel<List<AgendaModel>>>> EditInformationByEmail(string email, AgendaUpdateDto agendaUpdateDto)
        {
            var informations = await _agendaInterface.EditInformationByEmail(email, agendaUpdateDto);
            return Ok(informations);
        }

        [HttpDelete("DeleteInformation/{idInformation}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ResponseModel<List<AgendaModel>>>> DeleteInformation(int idInformation)
        {
            var informations = await _agendaInterface.DeleteInformation(idInformation);
            return Ok(informations);
        }

        [HttpDelete("DeleteInformationByEmail")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ResponseModel<List<AgendaModel>>>> DeleteInformationByEmail(string emailInformation)
        {
            var informations = await _agendaInterface.DeleteInformationByEmail(emailInformation);
            return Ok(informations);
        }

    }
}
