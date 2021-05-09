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
using Microsoft.AspNetCore.Http;

namespace Api.Controllers
{
   [Authorize]
    public class UsersController:BaseApiController
    {
        private readonly IUserRepository _userRepository;
       private readonly IMapper _mapper;
       private readonly IPhotoService _photoservice;
        public UsersController(IUserRepository userRepository,IMapper mapper,IPhotoService photoservice)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _photoservice = photoservice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {

            return Ok(await  _userRepository.GetUsersAsync());
        }

         [HttpGet("{username}",Name="GetUser")]
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
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

           _mapper.Map(memberUpdateDto,user);

            _userRepository.Update(user);

            if(await _userRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to update user");

        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
           
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            var result =await _photoservice.AddPhotoAsync(file);

            if(result.Error != null) 
            return BadRequest(result.Error.Message);

                var photo = new Photo {
                    Url = result.SecureUrl.AbsoluteUri,
                    PublicId = result.PublicId,
                };

            if(user.Photos.Count == 0)
            {
                photo.IsMain = true;

            }
            user.Photos.Add(photo);

            if(await _userRepository.SaveAllAsync())
            {
                return CreatedAtRoute("GetUser",new {username= user.UserName},_mapper.Map<PhotoDto>(photo));
            }
           // return _mapper.Map<PhotoDto>(photo);

            return BadRequest("Problem adding photo");


        }

        [HttpPut("set-main-photo/{photoid}")]
        public async Task<ActionResult>  SetMainPhoto(int photoid)
        {
            var user =await _userRepository.GetUserByUsernameAsync(User.GetUsername());

           var photo =  user.Photos.FirstOrDefault(x =>x.Id == photoid);

           if (photo.IsMain) return BadRequest("This is already your main photo");

           var currentmain = user.Photos.FirstOrDefault(x =>x.IsMain);
           if(currentmain != null) currentmain.IsMain =false;
           photo.IsMain = true;
           
           if(await _userRepository.SaveAllAsync()) return NoContent();
           return BadRequest("failed to set main pohto");
        }

        [HttpDelete("delete-photo/{photoid}")]
        public async Task<ActionResult> DeletePhoto(int photoid)
        {
            var user =await  _userRepository.GetUserByUsernameAsync(User.GetUsername());

            var photo = user.Photos.FirstOrDefault(x=>x.Id == photoid);

            if(photo == null) return NotFound();

            if(photo.IsMain) return BadRequest("can not delete photo");

            if(photo.PublicId != null)
            {
               var result = await _photoservice.DeletePhotoAsync(photo.PublicId);
                    if(result.Error != null) return BadRequest(result.Error.Message);

            }
            user.Photos.Remove(photo);
            if(await _userRepository.SaveAllAsync()) 
            return Ok();
            return BadRequest("failed to delete photo");
        }
        
    }
}