using MusicPortal_Asp.Net_MVC_.BLL.DTO;

namespace MusicPortal_Asp.Net_MVC_.BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> CreateUser(UserDTO userDto);
        Task UpdateUser(UserDTO userDto);
        Task DeleteUser(int id);
        Task<UserDTO> GetUser(int id);
        Task<IEnumerable<UserDTO>> GetUsers();
        Task<bool> ExistsUser(int id);
        Task<IEnumerable<UserDTO>> GetInactiveUsers();
        Task ChangeActiveStatus(int id);

        Task<UserDTO> GetUserByLogin(string login);
        Task<bool> AnyUsers();


    }
}
