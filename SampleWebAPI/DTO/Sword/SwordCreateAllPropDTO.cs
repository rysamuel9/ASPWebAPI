using SampleWebAPI.DTO.Element;

namespace SampleWebAPI.DTO.Sword
{
    public class SwordCreateAllPropDTO
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public List<ElementDTO> Elements { get; set; }
        public TypeSwordDTO typeSword { get; set; }
    }
}
