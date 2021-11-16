using AutoMapper;
using MusicQuizAPI.Models.Database;
using MusicQuizAPI.Models.Dtos;

namespace MusicQuizAPI.Models.Profiles
{
    public class AnimeProfile : Profile
    {
        public AnimeProfile()
        {
            CreateMap<Anime, AnimeReadDto>();
        }
    }
}