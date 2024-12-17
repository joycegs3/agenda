using System.Collections.Generic;
using AgendaWebApplication.Data;
using AgendaWebApplication.Dto.Agenda;
using AgendaWebApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace AgendaWebApplication.Services.Agenda
{
    public class AgendaService : IAgendaInterface
    {
        private readonly AppDbContext _context;
        public AgendaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<AgendaModel>> GetInformationByPhoneNumber(string phoneNumber)
        {
            ResponseModel<AgendaModel> response = new ResponseModel<AgendaModel>();

            try
            {
                var information = await _context.Agenda.Include(u => u.User).FirstOrDefaultAsync(informationDatabase => informationDatabase.PhoneNumber.Equals(phoneNumber));

                if (information == null)
                {
                    response.Message = "Nenhum contato encontrado!";

                    return response;
                }

                response.Data = information;
                response.Message = "Contato encontrado!";

                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<AgendaModel>> GetInformationById(int idInformation)
        {
            ResponseModel<AgendaModel> response = new ResponseModel<AgendaModel>();

            try
            {
                var information = await _context.Agenda.Include(u => u.User).FirstOrDefaultAsync(informationDatabase => informationDatabase.Id == idInformation);

                if (information == null)
                {
                    response.Message = "Nenhum contato encontrado!";
                    
                    return response;
                }

                response.Data = information;
                response.Message = "Contato encontrado!";

                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<List<AgendaModel>>> ListInformations()
        {
            ResponseModel<List<AgendaModel>> response = new ResponseModel<List<AgendaModel>>();

            try
            {
                var informations = await _context.Agenda.Include(u => u.User).ToListAsync();

                response.Data = informations;
                response.Message = "Contatos da agenda coletados com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;

                return response;
                
            }
        }

        public async Task<ResponseModel<List<AgendaModel>>> CreateInformation(AgendaCreationDto agendaCreationDto)
        {
            ResponseModel<List<AgendaModel>> response = new ResponseModel<List<AgendaModel>>();

            try
            {
                var information = new AgendaModel()
                {
                    Name = agendaCreationDto.Name,
                    Email = agendaCreationDto.Email,
                    PhoneNumber = agendaCreationDto.PhoneNumber
                };

                _context.Agenda.Add(information);
                await _context.SaveChangesAsync();

                response.Data = await _context.Agenda.ToListAsync();
                response.Message = "Contato criado com sucesso!";

                return response;

            }
            catch (Exception ex)
            {
                response.Message= ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<List<AgendaModel>>> EditInformation(AgendaUpdateDto agendaUpdateDto)
        {
            ResponseModel<List<AgendaModel>> response = new ResponseModel<List<AgendaModel>>();

            try
            {
                var information = await _context.Agenda.Include(u => u.User).FirstOrDefaultAsync(informationDatabase => informationDatabase.Id == agendaUpdateDto.Id);

                if (information == null)
                {
                    response.Message = "Esse contato não existe!";

                    return response;
                }

                information.Name = agendaUpdateDto.Name;
                information.Email = agendaUpdateDto.Email;
                information.PhoneNumber = agendaUpdateDto.PhoneNumber;

                _context.Update(information);
                await _context.SaveChangesAsync();

                response.Data = await _context.Agenda.Include(u => u.User).ToListAsync();
                response.Message = "Contato editado com sucesso!";

                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<List<AgendaModel>>> DeleteInformation(int idIfnormation)
        {
            ResponseModel<List<AgendaModel>> response = new ResponseModel<List<AgendaModel>>();

            try 
            {
                var information = await _context.Agenda.Include(u => u.User).FirstOrDefaultAsync(informationDatabase => informationDatabase.Id == idIfnormation);
                
                if (information == null)
                {
                    response.Message = "Esse contato não existe!";

                    return response;
                }

                _context.Agenda.Remove(information);
                await _context.SaveChangesAsync();

                response.Data = await _context.Agenda.Include(u => u.User).ToListAsync();
                response.Message = "Contato removido com sucesso!";

                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<List<AgendaModel>>> EditInformationByEmail(string email, AgendaUpdateDto agendaUpdateDto)
        {
            ResponseModel<List<AgendaModel>> response = new ResponseModel<List<AgendaModel>>();

            try
            {
                var information = await _context.Agenda.Include(u => u.User).FirstOrDefaultAsync(informationDatabase => informationDatabase.Email == email);

                if (information == null)
                {
                    response.Message = "Esse contato não existe!";

                    return response;
                }

                information.Name = agendaUpdateDto.Name;
                information.Email = agendaUpdateDto.Email;
                information.PhoneNumber = agendaUpdateDto.PhoneNumber;

                _context.Update(information);
                await _context.SaveChangesAsync();

                response.Data = await _context.Agenda.Include(u => u.User).ToListAsync();
                response.Message = "Contato editado com sucesso!";

                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<List<AgendaModel>>> DeleteInformationByEmail(string emailInformation)
        {
            ResponseModel<List<AgendaModel>> response = new ResponseModel<List<AgendaModel>>();

            try
            {
                var information = await _context.Agenda.Include(u => u.User).FirstOrDefaultAsync(informationDatabase => informationDatabase.Email == emailInformation);

                if (information == null)
                {
                    response.Message = "Esse contato não existe!";

                    return response;
                }

                _context.Agenda.Remove(information);
                await _context.SaveChangesAsync();

                response.Data = await _context.Agenda.Include(u => u.User).ToListAsync();
                response.Message = "Contato removido com sucesso!";

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
