using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SenatApi;


namespace SenatWebAp
{
    public class IssueController : ApiController
    {
        RestSenatApiClient client = new RestSenatApiClient("https://dev.senat.sbt-osop-224.sigma.sbrf.ru");

        [HttpGet]
        public List<PageOfIssueVersionIssuesListItemDto> GetIssues()
        {
            return client.GetListOfIssues();
        }

        [HttpGet]
        public IssueMultilingualDto GetIssue(Guid id)
        {
            return client.GetIssue(id);
        }
        // разобраться с отправкой
        [HttpPost]
        public Guid Post([FromBody] IssueDto issue)
        {
            return client.CreateIssue(issue);
        }
    }
}