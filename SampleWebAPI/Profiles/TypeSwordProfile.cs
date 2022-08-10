using AutoMapper;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO.Sword;
using SampleWebAPI.DTO.TypeSword;

namespace SampleWebAPI.Profiles
{
    public class TypeSwordProfile : Profile
    {
        public TypeSwordProfile()
        {
            CreateMap<TypeSword, TypeSwordDTO>();
            CreateMap<TypeSwordDTO, TypeSword>();

            CreateMap<TypeSword, TypeCreateDTO>();
            CreateMap<TypeCreateDTO, TypeSword>();

            CreateMap<TypeSword, TypeReadDTO>();
            CreateMap<TypeReadDTO, TypeSword>();

            CreateMap<TypeSword, TypeUpdateDTO>();
            CreateMap<TypeUpdateDTO, TypeSword>();

            CreateMap<TypeSword, TypeSwordTestDTO>();
            CreateMap<TypeSwordTestDTO, TypeSword>();
        }
    }
}
