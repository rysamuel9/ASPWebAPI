using SampleWebAPI.Domain;
using SampleWebAPI.DTO.Quote;

namespace SampleWebAPI.DTO.Samurai
{
    public class SamuraiWithQuotesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<QuoteReadDTO> Quotes { get; set; }
    }
}
