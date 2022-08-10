using SampleWebAPI.DTO.Sword;

namespace SampleWebAPI.DTO.Samurai
{
    public class SamuraiCreateAllPropDTO
    {
        public string Name { get; set; }
        public List<SwordCreateAllPropDTO> Swords { get; set; }
    }
}
