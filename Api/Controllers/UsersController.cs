using System.Linq;
using Api.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Api.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    
    public class UsersController:BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
       
        public async Task<ActionResult<IEnumerable<AppUsers>>> GetUsers()
        {
         return await _context.users.ToListAsync();
        }

         [HttpGet("{id}")]
          [Authorize]
        public async Task<ActionResult<AppUsers>> GetUsers(int id)
        {
         return await _context.users.FindAsync(id);
        }
    }
}