using System.Linq;
using Api.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Api.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Api.Interfaces;
using AutoMapper;
using Api.DTOs;
using System.Security.Claims;

namespace Api.Controllers
{
   [Authorize]
    public class UsersController:BaseApiController
    {
        private readonly IUserRepository _userRepository;
       private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {

            return Ok(await  _userRepository.GetUsersAsync());
        }

         [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUsers(string username)
        {
            var result = await _userRepository.GetMemberAsync(username);
            if(result != null)
            return result;
            return NotFound();

          
        }
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var usernaem = User.FindFirst(ClaimTypes.Name)?.Value;

            var user = await _userRepository.GetUserByUsernameAsync(usernaem);

           _mapper.Map(memberUpdateDto,user);

            _userRepository.Update(user);

            if(await _userRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to update user");

        }
        
    }
}