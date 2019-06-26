using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;


namespace SenatApi
{
    //public class SqlIssueMeetingRepository : IIssueMeetingRepository
    //{
    //    private SenatContext db;

    //    public SqlIssueMeetingRepository(SenatContext context)
    //    {
    //        db = context;
    //    }

    //    //public void Add(IssueMeeting dto)
    //    //{
    //    //    ConfigMapper.CreateModel();
    //    //    var issuesAndMembers = ConfigMapper.CreateIssuesAndMembers(dto);
    //    //    var meetings = ConfigMapper.CreateMeeting(dto);

    //    //    Random random = new Random();

    //    //    foreach (var meeting in meetings)
    //    //    {
    //    //        db.Meetings.Add(meeting);
    //    //    }

    //    //    foreach (var issue in issuesAndMembers.Item1)
    //    //    {
    //    //        issue.Meeting = meetings[random.Next(meetings.Count)];
    //    //        db.Issues.Add(issue);
    //    //    }

    //    //    foreach (var member in issuesAndMembers.Item2)
    //    //    {
    //    //        db.Members.Add(member);
    //    //    }

    //    //    db.SaveChanges();

    //    //}

    //    public IEnumerable<ShowQueryIssue> GetIssueQueryResults()
    //    {
    //        var queryIssue = db.Issues.Select(p => new ShowQueryIssue
    //        {
    //            Status = p.Status,
    //            MeetingStatus = p.Meeting == null ? null : (MeetingStatus?) p.Meeting.Status,
    //            Title = p.Title,
    //            Date = p.Meeting.Date,
    //            Num = p.Meeting.Num
    //        }).OrderBy(p => p.Date).ToList();
    //        return queryIssue;
    //    }

    //    public IEnumerable<ShowQueryMeeting> GetMeetingQueryResults(Guid? enterMGuid, DateTime? enterDate)
    //    {
    //        var queryMeeting = db.Issues
    //            .Where(x => x.Meeting != null)
    //            .Join(db.Members.Where(x => x.MemberId == enterMGuid),
    //                p => p.Id,
    //                x => x.IssueId,
    //                (p, x) => new
    //                {
    //                   p.MeetingId
    //                })
    //            .Distinct()
    //            .Join(db.Meetings, ids => ids.MeetingId, m => m.Id, (ids, m) => m)
    //            .Select(a => new ShowQueryMeeting()
    //            {
    //                MeetingId = a.Id,
    //                Date = a.Date,
    //                Num = a.Num,
    //                Issues = a.Issues.Count
    //            }).Where(c => c.Issues > 1 && (c.Date > enterDate || enterDate == null))
    //            .OrderBy(c => c.Date).ToList();
    //        db.Dispose();
    //        return queryMeeting;
    //    }
    //}
}
//{
        //    using (SenatContext db = new SenatContext())
        //    {
        //        var issueDB = db.Issues;
        //        var memberDB = db.Members;
        //        var queryMeeting = issueDB.Join(memberDB.Where(p=>p.MemberId == enterMGuid ),
        //            p=>p.Id,
        //            s=>s.IssueId,
        //               (p,s)=> new
        //               {

        //               })




        //        (p => new ShowQueryMeeting
        //        {
        //            Date = p.Date,
        //            Num = p.Num,
        //            Issues = p.Issues.Count
        //        }).OrderBy(p => p.Date).ToList();db.Dispose();
        //        return queryMeeting;

        //public IEnumerable<ShowQueryMeeting> GetMeetingQueryResults()
        //{
        //        var queryMeeting = db.Meetings.Select(p => new ShowQueryMeeting
        //        {
        //            Date = p.Date,
        //            Num = p.Num,
        //            Issues = p.Issues.Count
        //        }).OrderBy(p => p.Date).ToList();
        //        db.Dispose();
        //        return queryMeeting;
        //}

