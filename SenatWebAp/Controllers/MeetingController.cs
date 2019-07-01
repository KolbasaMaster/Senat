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
        private RestSenatApiClient_ _client;

        public MeetingController(RestSenatApiClient_ client)
        {
            _client = client;
        }

        [Authorize(Roles = "Initiator, User")]
        [HttpGet]
        public List<PageOfMeetingLocalizedDto> GetMeetings()
        {
           return _client.GetListOfMeetings();
        }

        [Authorize(Roles = "Initiator, User")]
        [HttpGet]
        public MeetingDto GetMeeting(Guid id)
        {
            return _client.GetMeeting(id);
        }

        [Authorize(Roles = "Initiator")]
        [HttpPost]
        public Guid PostMeeting(CreateMeetingDto meeting)
        {
            return _client.CreateMeeting(meeting);
        }
    }
}
