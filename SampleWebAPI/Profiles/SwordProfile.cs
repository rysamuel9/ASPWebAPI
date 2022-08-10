using AutoMapper;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO.Sword;

namespace SampleWebAPI.Profiles
{
    public class SwordProfile : Profile
    {
        public SwordProfile()
        {
            //Create Sword
            CreateMap<Sword, SwordCreateDTO>();
            CreateMap<SwordCreateDTO, Sword>();

            // Read All Sword
            CreateMap<Sword, SwordReadDTO>();
            CreateMap<SwordReadDTO, Sword>();

            // Update Sword
            CreateMap<Sword, SwordUpdateDTO>();
            CreateMap<SwordUpdateDTO, Sword>();

            CreateMap<Sword, SwordCreateAllPropDTO>();
            CreateMap<SwordCreateAllPropDTO, Sword>();

            CreateMap<Sword, SwordWithTypeDTO>();
            CreateMap<SwordWithTypeDTO, Sword>();

            CreateMap<Sword, SwordToExElementDTO>();
            CreateMap<SwordToExElementDTO, Sword>();

            CreateMap<Sword, SwordCreate2DTO>();
            CreateMap<SwordCreate2DTO, Sword>();

        }
    }
}
