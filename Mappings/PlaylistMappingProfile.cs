using AutoMapper;
using UBB_SE_2024_Music.Models;

namespace UBB_SE_2024_Music.Mappings
{
    public class PlaylistMappingProfile : Profile
    {
        public PlaylistMappingProfile()
        {
            CreateMap<Playlist, PlaylistForAddUpdateModel>().ReverseMap();
        }
    }
}
