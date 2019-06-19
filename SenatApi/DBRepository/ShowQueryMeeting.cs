using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenatApi
{
    public class ShowQueryMeeting
    {
        public Guid MeetingId { get; set; }
        public DateTime? Date { get; set; }
        public string Num { get; set; }
        public int Issues { get; set; }
    }
}
