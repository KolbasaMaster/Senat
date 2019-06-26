using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SenatApi;
using System.Data.Entity;

namespace SenatWebApi
{
    public class IssueController : ApiController
    {
        SenatContext db = new SenatContext();
        public IEnumerable<ModelIssue> GetIssues()
        {
            return db.Issues;
        }
        public ModelIssue GetIssue(Identific id)
        {
            ModelIssue issue = db.Issues.Find(id);
            return issue;
        }
        [HttpPost]
        public void CreateIssue([FromBody]ModelIssue issue)
        {
            db.Issues.Add(issue);
            db.SaveChanges();
        }
        [HttpPut]
        public void EditIssue(Guid id, [FromBody]ModelIssue issue)
        {
            if (id == issue.Id)
            {
                db.Entry(issue).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
