using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenatApi
{
    public class CollegialBodyDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CompanyRefDto Company { get; set; }
        public HoldingRefDto Holding { get; set; }
    }
}
