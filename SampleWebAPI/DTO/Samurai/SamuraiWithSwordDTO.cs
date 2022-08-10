using SampleWebAPI.DTO.Sword;

namespace SampleWebAPI.DTO.Samurai
{
    public class SamuraiWithSwordDTO
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public List<SwordCreateDTO> Swords { get; set; }
    }
}
