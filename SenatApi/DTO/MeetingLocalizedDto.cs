using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenatApi
{
    public enum MeetingStatusDto { Draft, Prepared, Opened, Closed};
    public class MeetingLocalizedDto
    {
        public Guid Id { get; set; }
        public string Num { get; set; }
        public HoldingDto Holding { get; set; }
        public CollegialBodyDto CollegialBody {get;set;}
        public MeetingStatusDto Status { get; set; }
        public VotingStatisticDto Voting { get; set; }
        public bool HasProtocol { get; set; }
        public DateTime CreateDate { get; set; }
        private string _type { get; set; }
    }
}
