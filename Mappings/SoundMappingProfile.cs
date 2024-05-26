using AutoMapper;
using UBB_SE_2024_Music.Models;

namespace UBB_SE_2024_Music.Mappings
{
    public class SoundMappingProfile : Profile
    {
        public SoundMappingProfile()
        {
            CreateMap<Sound, SoundForAddUpdateModel>().ReverseMap();
        }
    }
}
