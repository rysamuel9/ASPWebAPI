using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Domain
{
    public class TypeSword
    {
        public int Id { get; set; }
        public string TypeName { get; set; }

        public Sword Sword { get; set; }
        public int SwordId { get; set; }
    }
}
