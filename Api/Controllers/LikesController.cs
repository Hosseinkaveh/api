using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Entities;
using Api.Extension;
using Api.Helpers;
using Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    public class LikesController : BaseApiController
    {
        private readonly IUserRepository _UserRepository ;
        private readonly ILikesRepository _LikesRepository ;
        public LikesController(IUserRepository userRepository, ILikesRepository likesRepository)
        {
            _UserRepository = userRepository;
            _LikesRepository = likesRepository;
        }

        [HttpPost("{username}")]
       public async Task<ActionResult> AddLike(string username)
       {
           var sourceUserId = User.GetUserId();
           var likedUser = await _UserRepository.GetUserByUsernameAsync(username);
           var sourceUser = await _LikesRepository.GetUserWithLike(sourceUserId);

           if(likedUser == null) return NotFound();

           if(sourceUser.UserName == username) return BadRequest("you can not like your selfe");

           var userLike = await _LikesRepository.GetUserLike(sourceUserId,likedUser.Id);

           if(userLike != null) return BadRequest("you aleardy like this user");

           userLike = new UserLike{
               SourceUesrId=sourceUserId,
               LikedUserId = likedUser.Id,
           };
           sourceUser.LikedUsers.Add(userLike);

           if(await _UserRepository.SaveAllAsync()) return Ok();

           return BadRequest("Failed to Like user");
           
       }

        [HttpGet]
       public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLikes([FromQuery]LikesParams likesParams)
       {
           likesParams.UserId = User.GetUserId();
           var users = await  _LikesRepository.GetUserLikes(likesParams);

           Response.AddPageinationHeaders(users.CurrentPage,users.PageSize,users.TotalCount,users.TotalPage);

           return Ok(users);
       }
    }
}