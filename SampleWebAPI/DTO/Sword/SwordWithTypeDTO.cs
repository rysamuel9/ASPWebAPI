using SampleWebAPI.DTO.TypeSword;

namespace SampleWebAPI.DTO.Sword
{
    public class SwordWithTypeDTO
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public int SamuraiId { get; set; }
        public TypeSwordTestDTO TypeSword { get; set; }
    }
}
