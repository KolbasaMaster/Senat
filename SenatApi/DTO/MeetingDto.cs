using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenatApi
{
    public class MeetingDto
    {
        public string _discriminator { get; set; }
        public Identific collegialBody { get; set; }
        public string num { get; set; }
        public MeetingStatus status { get; set; }
        public string agendaDueDate { get; set; }
        public string materialsDueDate { get; set; }
        public string date { get; set; }
        public Dictionary<string, string> address;
        public List<Identific> invitedPersons { get; set; }
        public List<Identific> issues { get; set; }
    }
}
