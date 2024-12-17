using AgendaWebApplication.Models;
using AgendaWebApplication.Dto;
using AgendaWebApplication.Dto.User;
using Microsoft.AspNetCore.SignalR;

namespace AgendaWebApplication.Services.User
{
    public interface IUserInterface
    {
        Task<UserModel> CreateUser(UserModel user);
        Task<ResponseModel<UserModel>> GetCurentUser(int userId);
        Task<ResponseModel<List<UserModel>>> ListUsers();
        Task<UserModel> GetUserByEmailAndPassword(UserModel user);
        Task<ResponseModel<UserModel>> DeleteUser(int idUser);

    }
}
