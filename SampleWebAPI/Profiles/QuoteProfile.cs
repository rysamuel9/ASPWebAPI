using AutoMapper;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO.Quote;

namespace SampleWebAPI.Profiles
{
    public class QuoteProfile : Profile
    {
        public QuoteProfile()
        {
            //Create Quote
            CreateMap<Quote, QuoteCreateDTO>();
            CreateMap<QuoteCreateDTO, Quote>();

            // Read All Quote
            CreateMap<Quote, QuoteReadDTO>();
            CreateMap<QuoteReadDTO, Quote>();

            // Update Quote
            CreateMap<Quote, QuoteUpdateDTO>();
            CreateMap<QuoteUpdateDTO, Quote>();
        }
    }
}
