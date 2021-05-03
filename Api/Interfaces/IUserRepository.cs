using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Entities;

namespace Api.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUsers user);

        Task<bool> SaveAllAsync();

        Task<IEnumerable<MemberDto>> GetUsersAsync();

        //Task<MemberDto> GetUserByIdAsync(int id);
        Task<AppUsers> GetUserByUsernameAsync(string username);
        Task<MemberDto> GetMemberAsync(string username);
    }
}