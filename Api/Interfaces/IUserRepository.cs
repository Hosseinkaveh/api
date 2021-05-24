using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Entities;
using Api.Helpers;

namespace Api.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUsers user);

        Task<bool> SaveAllAsync();

        Task<PageList<MemberDto>> GetUsersAsync(UserParams userParams);

        //Task<IEnumerable<AppUsers>> GetUsersAsync();

        Task<AppUsers> GetUserByUsernameAsync(string username);
        Task<AppUsers> GetUserByIdAsync(int id);
        Task<MemberDto> GetMemberAsync(string username);
    }
}