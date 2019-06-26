using System;
using System.Collections;
using System.Collections.Generic;

namespace SenatApi
{
  //  public enum IssueStatus { Preparing, Prepared, WaitingForConsideration, OnConsideration, OnVoting, Resolved, OnModification, Removed, Modified, PreparedInMeeting, NotConsidered };
    public class ModelIssueDto
    {
        public Guid Id { get; set; }
        public Identific CollegialBody { get; set; }
        public bool IsInformational { get; set; }
        public Dictionary<string, string> Title { get; set; }
        public Dictionary <string, string>Description { get; set; }
        public string Estimate { get; set; }
        public IssueStatus Status { get; set; }

        public virtual ModelMeeting Meeting { get; set; }
        public Guid? MeetingId { get; set; }
        public ICollection<ModelMember> ModelMember { get; set; }
        public ModelIssueDto()
        {
            ModelMember = new List<ModelMember>();
        }

}
   
  
}
