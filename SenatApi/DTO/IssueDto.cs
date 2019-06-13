using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenatApi
{
    public class IssueDto
    {
        public IssueStatus status { get; set; }
        public Identific collegialBody { get; set; }
        public bool isInformational { get; set; }
        public Dictionary<string, string> title;
        public Dictionary<string, string> description;
        public string estimate { get; set; }
        public List<object> speakers { get; set; }
        public List<Identific> initiators { get; set; }
        public List<Identific> invitees { get; set; }
        public List<object> materials { get; set; }
    }
}
