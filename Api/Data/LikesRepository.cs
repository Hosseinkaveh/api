using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Entities;
using Api.Extension;
using Api.Helpers;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class LikesRepository : ILikesRepository
    {
        private readonly DataContext _context;
        public LikesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserLike> GetUserLike(int sourceUserid, int LikedUserId)
        {
            return await _context.Likes.FindAsync(sourceUserid,LikedUserId);
        }

        public async Task<PageList<LikeDto>> GetUserLikes(LikesParams likesParams)
        {
            var user = _context.users.OrderBy(x=>x.UserName).AsQueryable();
            var likes = _context.Likes.AsQueryable();

            if(likesParams.predicate == "liked")
            {
                likes = likes.Where(like=>like.SourceUesrId == likesParams.UserId);
                user = likes.Select(like=>like.LikedUser);
            }
            if(likesParams.predicate == "likedBy")
           {
            likes = likes.Where(like=>like.LikedUserId == likesParams.UserId);
            user = likes.Select(like=>like.SourceUser);
           }

            var likeusers = user.Select(user => new LikeDto{
                Username = user.UserName,
                KnownAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(p =>p.IsMain).Url,
                City = user.City,
                Id = user.Id,
            });
            return await PageList<LikeDto>.CreateAsync(likeusers,likesParams.PageNumber,likesParams.PageSize);

        }

        public async Task<AppUsers> GetUserWithLike(int userId)
        {
            return await _context.users
            .Include(x =>x.LikedUsers)
            .FirstOrDefaultAsync(x=>x.Id == userId);
        }
    }
}