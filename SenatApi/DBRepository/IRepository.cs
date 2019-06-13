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
        IEnumerable<ShowQueryMeeting> GetMeetingQueryResults();
        
    }

    //public class IssueQueryResult
    //{
    //    public Guid IssueId { get; set; }
    //    public string MeetingNum { get; set; }
    //}

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
        }

        public IEnumerable<ShowQueryIssue> GetIssueQueryResults()
        {
            using (SenatContext db = new SenatContext())
            {
                var issueDB = db.Issues;

                var queryIssue = issueDB.Select(p => new
                {
                    p.Status,
                    MeetingStatus = p.Meeting == null ? null : (MeetingStatus?)p.Meeting.Status,
                    p.Title,
                    p.Meeting.Date,
                    p.Meeting.Num

                }).OrderBy(p => p.Date);
                var IssueQueryList = queryIssue
                    .ToList()
                    .Select(x => new ShowQueryIssue
                    {
                        Date = x.Date,
                        MeetingStatus = x.MeetingStatus,
                        Status = x.Status,
                        Title = x.Title,
                        Num = x.Num
                    });
                return IssueQueryList;
            }
        }
        public IEnumerable<ShowQueryMeeting> GetMeetingQueryResults()
        {
            using (SenatContext db = new SenatContext())
            {
                var meetingDB = db.Meetings;
                var queryMeeting = meetingDB.Select(p => new
                {
                    p.Status,
                    p.Date,
                    p.Num,
                    p.Issues.Count
                }).OrderBy(p => p.Date);
                var MeetingQueryList = queryMeeting
                    .ToList().Select(x => new ShowQueryMeeting
                    {
                        Status = x.Status,
                        Date = x.Date,
                        Num = x.Num,
                        Issues = x.Count
                    });
                return MeetingQueryList;
            }
        }      
    }
}
