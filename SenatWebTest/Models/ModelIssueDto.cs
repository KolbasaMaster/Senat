using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SenatApi;

namespace SenatWebTest
{
    public class ModelIssueDto1
    {
        public Guid Id { get; set; }
        public Guid CollegialBody { get; set; }
        public bool IsInformational { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Estimate { get; set; }
        public IssueStatus Status { get; set; }
        public Guid  Meeting { get; set; }      
        
       
    }
}