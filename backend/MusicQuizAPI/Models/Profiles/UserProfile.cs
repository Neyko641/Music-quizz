using AutoMapper;
using MusicQuizAPI.Models.Database;
using MusicQuizAPI.Models.Dtos;

namespace MusicQuizAPI.Models.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDto>()
                .ForMember(dest => dest.RegisteredDate,
                    opts => opts.MapFrom(src => src.RegisteredDate.ToString("d MMM yyyy")));

            CreateMap<User, UserSecuredReadDto>()
                .ForMember(dest => dest.RegisteredDate,
                    opts => opts.MapFrom(src => src.RegisteredDate.ToString("d MMM yyyy")));

            CreateMap<User, UserSimplifiedReadDto>();

            CreateMap<Friendship, FriendshipReadDto>();
        }
    }
}