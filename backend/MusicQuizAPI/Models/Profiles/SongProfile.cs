using AutoMapper;
using MusicQuizAPI.Models.Database;
using MusicQuizAPI.Models.Dtos;

namespace MusicQuizAPI.Models.Profiles
{
    public class SongProfile : Profile
    {
        public SongProfile()
        {
            CreateMap<Song, SongReadDto>()
                .ForMember(dest => dest.AnimeTitle,
                    opts => opts.MapFrom(src => src.Anime.Title));
        }
    }
}