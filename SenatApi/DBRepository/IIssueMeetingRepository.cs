using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenatApi
{

    public interface IIssueMeetingRepository
    {
        void Add(IssueMeeting dto);
        // to simplify use class instead of interface
        IEnumerable<ShowQueryIssue> GetIssueQueryResults();
        IEnumerable<ShowQueryMeeting> GetMeetingQueryResults(Guid? enterMGuid, DateTime? enterDate);
    }
}
