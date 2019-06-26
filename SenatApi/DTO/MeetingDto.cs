using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenatApi
{

    public class MeetingDto
    {
        public Guid Id { get; set; }
        public CollegialBodyDetailDto CollegialBody { get; set; }
        public HoldingDto Holding { get; set; }
        public string Num { get; set; }
        public DateTime AgendaDueDate { get; set; }
        public DateTime MaterialsDueDate { get; set; }
        public DateTime CreateDate { get; set; }
        public MeetingStatusDto Status {get;set;}
        public bool HasProtocol { get; set; }
        public MaterialDto Protocol { get; set; }
        private string _discriminator { get; set; }

        //public string _discriminator { get; set; }
        //public Identific collegialBody { get; set; }
        //public string num { get; set; }
        //public MeetingStatus status { get; set; }
        //public string agendaDueDate { get; set; }
        //public string materialsDueDate { get; set; }
        //public string date { get; set; }
        //public Dictionary<string, string> address;
        //public List<Identific> invitedPersons { get; set; }
       
        //public List<Identific> issues { get; set; }
    }
}
