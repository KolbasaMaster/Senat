using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenatApi.DTO
{
    public enum IssueMaterialTypeDto { DecisionProject, Presentation, Other, Attachment };
    public class MaterialWithCategoryDto
    {
        public MaterialRefDto Material { get; set; }
        public IssueMaterialTypeDto Category { get; set; }
        public List<MaterialRefDto> DraftsToMerge { get; set; }
    }
}
