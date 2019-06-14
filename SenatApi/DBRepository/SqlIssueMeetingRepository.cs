using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenatApi
{
    public class SqlIssueMeetingRepository : IIssueMeetingRepository
    {

        public void Add(IssueMeeting dto)
        {
            ConfigMapper.CreateModel();
            var issuesAndMembers = ConfigMapper.CreateIssuesAndMembers(dto);
            var meetings = ConfigMapper.CreateMeeting(dto);

            SenatContext context = new SenatContext();
            Random random = new Random();

            foreach (var meeting in meetings)
            {
                context.Meetings.Add(meeting);
            }

            foreach (var issue in issuesAndMembers.Item1)
            {
                issue.Meeting = meetings[random.Next(meetings.Count)];
                context.Issues.Add(issue);
            }

            foreach (var member in issuesAndMembers.Item2)
            {
                context.Members.Add(member);
            }

            context.SaveChanges();
            context.Dispose();
        }

        public IEnumerable<ShowQueryIssue> GetIssueQueryResults()
        {
            using (SenatContext db = new SenatContext())
            {
                var issueDB = db.Issues;

                var queryIssue = issueDB.Select(p => new ShowQueryIssue
                {
                    Status = p.Status,
                    MeetingStatus = p.Meeting == null ? null : (MeetingStatus?)p.Meeting.Status,
                    Title = p.Title,
                    Date = p.Meeting.Date,
                    Num = p.Meeting.Num
                }).OrderBy(p => p.Date).ToList();
                return queryIssue;


            }
        }
        public IEnumerable<ShowQueryMeeting> GetMeetingQueryResults()
        {
            using (SenatContext db = new SenatContext())
            {
                var meetingDB = db.Meetings;
                var queryMeeting = meetingDB.Select(p => new ShowQueryMeeting
                {
                    Status = p.Status,
                    Date = p.Date,
                    Num = p.Num,
                    Issues = p.Issues.Count
                }).OrderBy(p => p.Date).ToList();
                return queryMeeting;

            }
        }
    }
}
