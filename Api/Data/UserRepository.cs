using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Entities;
using Api.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context,IMapper mapper)
        {
           _context = context;
           _mapper= mapper;

        }

        public async Task<AppUsers> GetUserByUsernameAsync(string username)
        {
            return await _context.users
            .Where(x => x.UserName == username.ToLower())
            .Include(x =>x.Photos)
            .SingleOrDefaultAsync();
        }
        public async Task<MemberDto> GetMemberAsync(string username)
        {
           return await _context.users
            .Where(x => x.UserName == username.ToLower())
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetUsersAsync()
        {
          return await _context.users
          .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
          .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
          return await _context.SaveChangesAsync()>0;
        }

        public void Update(AppUsers user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}