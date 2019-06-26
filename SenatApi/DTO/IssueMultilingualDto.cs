using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SenatApi
{
    public class IssueMultilingualDto
    {
        public Guid Id { get; set; }
        public CollegialBodyDto CollegialBody { get; set; }
        public Dictionary<string,string> Title { get; set; }
        public Dictionary<string,string> Description { get; set; }
        public List<LabelDto> Labels { get; set; }
        public TimeSpan Estimate { get; set; }
        public bool isConfidential { get; set; }
    }
}
