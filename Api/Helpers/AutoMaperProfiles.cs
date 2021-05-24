using System.Linq;
using Api.DTOs;
using Api.Entities;
using Api.Extension;
using AutoMapper;

namespace Api.Helpers
{
    public class AutoMaperProfiles:Profile
    {
        public AutoMaperProfiles()
        {
            CreateMap<AppUsers,MemberDto>()
            .ForMember(dest =>dest.PhotoUrl,opt =>opt.MapFrom(src =>
            src.Photos.FirstOrDefault(x =>x.IsMain).Url))
            .ForMember(dest =>dest.Age,opt =>opt.MapFrom(src =>src.DateOfBirth.CalculateAge()));
            
            CreateMap<Photo,PhotoDto>();

            CreateMap<MemberUpdateDto,AppUsers>();
            CreateMap<RegisterDto,AppUsers>();
        }
        
    }
}