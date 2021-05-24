using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Entities;
using Api.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Api.Helpers;
using System;

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
          public async Task<AppUsers> GetUserByIdAsync(int id)
        {
           return await _context.users.FindAsync(id);
        }
        public async Task<MemberDto> GetMemberAsync(string username)
        {
           return await _context.users
            .Where(x => x.UserName == username.ToLower())
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        public async Task<PageList<MemberDto>> GetUsersAsync(UserParams userParams)
        {
          var query  =  _context.users.AsQueryable();
         
        query =  query.Where(x=>x.UserName != userParams.CurrentUsername);
         query =  query.Where(x=>x.Gender == userParams.Gender); 

         var minDob = DateTime.Now.AddYears(-userParams.MaxAge-1);
         var maxDob = DateTime.Now.AddYears(-userParams.MinAge);

         query = query.Where(u=>u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);

         query = userParams.orderBy switch
         {
           "created" => query.OrderByDescending(u=>u.Create),
           _ => query.OrderByDescending(u=>u.LastActive)
         };

          return await PageList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>(_mapper
          .ConfigurationProvider).AsNoTracking(),userParams.PageNumber,userParams.PageSize);
           
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