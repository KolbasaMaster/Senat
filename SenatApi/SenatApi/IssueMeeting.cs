using System.Collections.Generic;

namespace SenatApi
{
   public class IssueMeeting
    {
        public List<IssueDto> issues { get; set; }
        public List<MeetingDto> meetings { get; set; }

    }
}
