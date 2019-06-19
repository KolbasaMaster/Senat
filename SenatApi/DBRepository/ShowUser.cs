using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenatApi
{
    public class ShowUser
    {
        public static void ShowUserIssues(IEnumerable<ShowQueryIssue> showQueryIssue)
        {
            Console.WriteLine("Status\t\t\tMeetingStatus \t\t\t \t\tTitle\t\t\t\tMeetingDate\t\t\t" +
               "MeetingNumber");
            foreach (var c in showQueryIssue)
                Console.WriteLine("{0}\t\t\t{1}\t\t\t{2}\t\t{3}\t\t{4}", c.Status, c.MeetingStatus, c.Title, c.Date, c.Num);
        }
        public static void ShowUserMeetings(IEnumerable<ShowQueryMeeting> showQueryMeeting)
        {
            Console.WriteLine("date\t\t\t \t\tNumber\t\t Count of Issues");
            foreach (var c in showQueryMeeting)
                Console.WriteLine("{0}\t\t {1}\t\t {2}\t\t\t ", c.Date, c.Num, c.Issues);
        }
    }
}
