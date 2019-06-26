using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenatApi
{
    public enum MaterialCategory { Presentation, DecisionProject, Other, Protocol, Attachment };
    public class MaterialDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public MaterialCategory Category {get;set;}
        public MaterialVersionDto CurrentVersion { get; set; }
    }
}
