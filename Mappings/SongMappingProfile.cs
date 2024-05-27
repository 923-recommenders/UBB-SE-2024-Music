using AutoMapper;
using UBB_SE_2024_Music.Models;

namespace UBB_SE_2024_Music.Mappings
{
    public class SongMappingProfile : Profile
    {
        public SongMappingProfile()
        {
            CreateMap<Song, SongForAddUpdateModel>().ReverseMap();
            CreateMap<Song, SongFeatures>().ReverseMap();
        }
    }
}
