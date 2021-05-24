using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Entities;
using Api.Helpers;

namespace Api.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int sourceUserid,int LikedUserId);
        Task<AppUsers> GetUserWithLike(int userId);
        Task<PageList<LikeDto>> GetUserLikes(LikesParams likesParams);
    }
}