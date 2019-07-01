using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SenatApi.DTO;

namespace SenatApi
{
    public class IssueDto
    {
        public CollegialBodyRefDto CollegialBody { get; set; }
        public Dictionary<string,string> Title { get; set; }
        public Dictionary< string,string> Description { get; set; }
        public List<LabelDto> Labels { get; set; }
        public string Estimate { get; set; }
        public List<EmployeeRefDto> Initiators { get; set; }
        public List<EmployeeRefDto> Speakers { get; set; }
        public List<EmployeeRefDto> Invitees { get; set; }
        public List<MaterialWithCategoryDto> Materials { get; set; }
        public bool isConfidential { get; set; }
    }
}
