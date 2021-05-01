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
            var result = await _userRepository.GetUserByUsernameAsync(username);
            if(result != null)
            return result;
            return NotFound();

          
        }
        //   [HttpGet("{id}")]
        // public async Task<ActionResult<MemberDto>> GetUsers(int id)
        // {
        //     return await _userRepository.GetUserByIdAsync(id);

          
        // }
    }
}