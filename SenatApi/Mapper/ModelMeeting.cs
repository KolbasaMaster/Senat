using System;
using System.Collections.Generic;

namespace SenatApi
{
    public enum MeetingStatus { Draft, Prepared, Opened, Closed }
    public class ModelMeeting
    {
        public Guid Id { get; set; }
        public string Discriminator { get; set; }
        public Guid CollegialBody { get; set; }
        public string Num { get; set; } 
        public string AgendaDueDate { get; set; }
        public string MaterialsDueDate { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public MeetingStatus Status { get; set; }
        public ICollection<ModelIssue> Issues { get; set; }
        
    }
}
