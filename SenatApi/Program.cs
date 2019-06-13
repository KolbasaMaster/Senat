using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using NLog;
using AutoMapper;


namespace SenatApi

{
    
    public class Program
    {
        static string json = File.ReadAllText(@"Data.json", Encoding.GetEncoding(1251));
        private static Logger logger = LogManager.GetCurrentClassLogger();     
        static QueryAboutPerson Query = new QueryAboutPerson();
        static SqlIssueMeetingRepository Add = new SqlIssueMeetingRepository();
        static IEnumerable<ShowQueryMeeting> showQueryMeetings = new List<ShowQueryMeeting>();
        static IEnumerable<ShowQueryIssue> showQueryIssue = new List<ShowQueryIssue>();

        public static void Main()
        {
            IssueMeeting issueAndMeeting = JsonConvert.DeserializeObject<IssueMeeting>(json);
            Add.Add(issueAndMeeting);
            showQueryIssue = Add.GetIssueQueryResults();
            showQueryMeetings = Add.GetMeetingQueryResults();
            Console.WriteLine("Status\t\t\t date\t\t\t \t\tNumber\t\t Count of Issues");
            foreach (var c in showQueryMeetings)
                Console.WriteLine("{0}\t\t {1}\t\t {2}\t\t\t {3}", c.Status, c.Date, c.Num, c.Issues);

            Console.WriteLine("Status\t\t\tMeetingStatus \t\t\t \t\tTitle\t\t\t\tMeetingDate\t\t\t\t" +
                "MeetingNumber");
            foreach (var c in showQueryIssue)
                Console.WriteLine("{0}\t\t\t{1}\t\t\t{2}\t\t{3}\t\t{4}", c.Status, c.MeetingStatus, c.Title, c.Date, c.Num);
            Query.QueryAboutPersons();
            Console.ReadKey();

            /*   ISenatApiClient client = new RestSenatApiClient("https://dev.senat.sbt-osop-224.sigma.sbrf.ru");

               var issueIds = issueAndMeeting.issues
                   .Select(issue => client.CreateIssue(issue));

               // if more than one meeting take only first one
               var meeting = issueAndMeeting.meetings.FirstOrDefault();
               if (meeting == null)
               {
                   throw new ArgumentException("No meetings in source file");
               }

               meeting.issues = issueIds
               .Select(issueId => new Identific { Id = issueId })
              .ToList();

               foreach (Guid g in issueIds)
               {
                   client.IssueToPrepeared(g);
               }
               client.CreateMeeteng(meeting);

      */





        }       
    }
}