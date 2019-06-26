using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenatApi
{
    public enum IssueAggregatedStatusDto { Preparing, Prepared, WaitingForConsideration, OnConsideration, OnVoting, Accepted, Decline, OnModification, Removed, Modified, PreparedInMeeting, NotConsidered, Considered };
    public class IssueVersionInIssuesListItemDto
    {
        public Guid Id { get; set; }
        public IssueAggregatedStatusDto IssueAggregatedStatusDto { get; set; }
        public string Title { get; set; }
        public IssueRefDto Issue { get; set; }
        public Int32 VersionNumber { get; set; }
        public CollegialBodyDto CollegialBody { get; set; }
        public EmployeeDto Initiator { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsConfidential { get; set; }
    }
}
