using AutoMapper;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO.Quote;
using SampleWebAPI.DTO.Samurai;

namespace SampleWebAPI.Profiles
{
    public class SamuraiProfile : Profile
    {
        public SamuraiProfile()
        {
            //CreateMap<SamuraiWithQuotesDTO, Samurai>();
            CreateMap<Samurai, SamuraiWithQuotesDTO>();

            CreateMap<Samurai, SamuraiWithSwordDTO>();
            CreateMap<SamuraiWithSwordDTO, Samurai>();

            CreateMap<Samurai, SamuraiCreateAllPropDTO>();
            CreateMap<SamuraiCreateAllPropDTO, Samurai>();

            CreateMap<Samurai, SamuraiReadDTO>();
            CreateMap<SamuraiReadDTO, Samurai>();

            CreateMap<SamuraiCreateDTO, Samurai>();

            CreateMap<QuoteReadDTO, Quote>();
            CreateMap<Quote, QuoteReadDTO>();
        }
    }
}
