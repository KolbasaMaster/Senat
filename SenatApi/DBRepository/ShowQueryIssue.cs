using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenatApi
{
    public class ShowQueryIssue
    {
        public IssueStatus Status { get; set; }
        public MeetingStatus? MeetingStatus { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Num { get; set; }
    }
}
