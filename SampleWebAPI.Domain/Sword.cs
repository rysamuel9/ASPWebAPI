using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Domain
{
    public class Sword
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public List<Element> Elements { get; set; }
        public TypeSword TypeSword { get; set; }
        public Samurai Samurai { get; set; }
        public int SamuraiId { get; set; }
    }
}
