
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
        static SenatContext context = new SenatContext();
        static string json = File.ReadAllText(@"Data.json", Encoding.GetEncoding(1251));
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static QueryAboutPerson queryAboutPerson = new QueryAboutPerson();
       // static IIssueMeetingRepository sqlIssueMeetingRepository = new SqlIssueMeetingRepository(context);
        static IEnumerable<ShowQueryMeeting> showQueryMeetings = new List<ShowQueryMeeting>();
        static IEnumerable<ShowQueryIssue> showQueryIssue = new List<ShowQueryIssue>();

        public static void Main(string[] args)
        {
            IssueMeeting issueAndMeeting = JsonConvert.DeserializeObject<IssueMeeting>(json);
            //sqlIssueMeetingRepository.Add(issueAndMeeting);
            //showQueryIssue = sqlIssueMeetingRepository.GetIssueQueryResults();
            //showQueryMeetings = sqlIssueMeetingRepository.GetMeetingQueryResults(Guid.Parse(args[0]), DateTime.Parse(args[1]));
            //ShowUser.ShowUserIssues(showQueryIssue);
            //ShowUser.ShowUserMeetings(showQueryMeetings);
            //queryAboutPerson.QueryAboutPersons();


            //  Console.ReadKey();

            var client = new RestSenatApiClient("https://dev.senat.sbt-osop-224.sigma.sbrf.ru");

            var issueIds = issueAndMeeting.issues
                .Select(issue => client.CreateIssue(issue));

            // if more than one meeting take only first one
            var meeting = issueAndMeeting.meetings.FirstOrDefault();
            if (meeting == null)
            {
                throw new ArgumentException("No meetings in source file");
            }

           // meeting.issues = issueIds
           // .Select(issueId => new Identific { Id = issueId })
           //.ToList();

           // foreach (Guid g in issueIds)
           // {
           //     client.IssueToPrepeared(g);
           // }
           // client.CreateMeeteng(meeting);







        }
    }
}
