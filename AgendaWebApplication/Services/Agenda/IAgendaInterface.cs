using AgendaWebApplication.Dto.Agenda;
using AgendaWebApplication.Models;

namespace AgendaWebApplication.Services.Agenda
{
    public interface IAgendaInterface
    {
        Task<ResponseModel<List<AgendaModel>>> ListInformations();
        Task<ResponseModel<AgendaModel>> GetInformationById(int idInformation);
        Task<ResponseModel<AgendaModel>> GetInformationByPhoneNumber(string phoneNumber);
        Task<ResponseModel<List<AgendaModel>>> CreateInformation(AgendaCreationDto agendaCreationDto);
        Task<ResponseModel<List<AgendaModel>>> EditInformation(AgendaUpdateDto agendaUpdateDto);
        Task<ResponseModel<List<AgendaModel>>> EditInformationByEmail(string email, AgendaUpdateDto agendaUpdateDto);
        Task<ResponseModel<List<AgendaModel>>> DeleteInformation(int idInformation);
        Task<ResponseModel<List<AgendaModel>>> DeleteInformationByEmail(string emailInformation);

    }
}
