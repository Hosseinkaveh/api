using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Api.Data;
using Api.DTOs;
using Api.Entities;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    public class AccountController:BaseApiController
    {
         private readonly DataContext _context;
         private readonly ITokenservice _tokenservice;
        public AccountController(DataContext context,ITokenservice tokenservice)

        {
            _context = context;
            _tokenservice = tokenservice;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)

        {
            if(await UserExists(registerDto.username)) return BadRequest("UserName is Taken");

            using var hmac = new HMACSHA512();  
            var user = new AppUsers{
                UserName = registerDto.username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
                PasswordSalt = hmac.Key
            };
            
       _context.users.Add(user);

        await _context.SaveChangesAsync();

        return new UserDto{
            username = user.UserName,
            token = _tokenservice.CreateToken(user)

        };
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
         var user = await _context.users
         .SingleOrDefaultAsync(x => x.UserName == loginDto.username.ToLower());

         if(user == null) return Unauthorized("Invalid Username or password");

         using var hmac = new HMACSHA512(user.PasswordSalt); 

         var ComputeHash =hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password)); 

         //check password
         for(int i=0;i<user.PasswordHash.Length;i++)
         {
             if(user.PasswordHash[i]!=ComputeHash[i]) return Unauthorized("Invalid username or Password");
             
         }
         
          return new UserDto{
            username = user.UserName,
            token = _tokenservice.CreateToken(user)

        };

        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.users.AnyAsync(x=>x.UserName == username.ToLower());
        }
        
    }
}