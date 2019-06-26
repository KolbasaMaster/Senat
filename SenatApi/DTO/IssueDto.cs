using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SenatApi
{
    public class IssueDto
    {        
        public IssueStatus Status { get; set; }        
        public Identific CollegialBody { get; set; }
        public bool IsInformational { get; set; }
        public Dictionary<string, string> Title { get; set; }
        public Dictionary<string, string> Description { get; set; }
        public string Estimate { get; set; }
        public List<object> Speakers { get; set; }
        public List<Identific> Initiators { get; set; }
        public List<Identific> Invitees { get; set; }
        public List<object> Materials { get; set; }
    }
}
