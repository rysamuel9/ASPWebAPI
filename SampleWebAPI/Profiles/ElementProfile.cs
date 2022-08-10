using AutoMapper;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO.Element;

namespace SampleWebAPI.Profiles
{
    public class ElementProfile : Profile
    {
        public ElementProfile()
        {
            //Create & Update Element
            CreateMap<Element, ElementDTO>();
            CreateMap<ElementDTO, Element>();

            // Read All Element
            CreateMap<Element, ElementReadDTO>();
            CreateMap<ElementReadDTO, Element>();
        }
    }
}
