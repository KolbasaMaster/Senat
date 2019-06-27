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
    public class MeetingController : ApiController
    {
        RestSenatApiClient client = new RestSenatApiClient("https://dev.senat.sbt-osop-224.sigma.sbrf.ru");

        public List<PageOfMeetingLocalizedDto> GetMeetings()
        {
           return client.GetListOfMeetings();
        }

        public MeetingDto GetMeeting(Guid id)
        {
            return client.GetMeeting(id);
        }
        public Guid PostMeeting(CreateMeetingDto meeting)
        {
            return client.CreateMeeting(meeting);
        }
    }
}
