using System;
using NUnit.Framework.Internal;
using AutoFixture;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using NUnit.Framework;
using SenatApi;

namespace SenatApi.UnitTests
{
    [TestFixture]
    public class UnitTest_QueryMeeting
    {
        private Fixture _fixture;
        private Mock<SenatContext> _mockDbContext;
        private List<ModelMeeting> _meetings;
        private List<Guid> _members;
        private List<ModelIssue> _issues;
        private List<ModelMember> _issueMembers;
        [SetUp]
        public void Setup()
        {
            Random random = new Random();
            _fixture = new Fixture();
            _meetings = _fixture.Build<ModelMeeting>()
                .With(x => x.Discriminator, "discriminator")
                .With(x => x.CollegialBody, Guid.NewGuid())
                .With(x => x.AgendaDueDate, "11.11.11")
                .With(x => x.MaterialsDueDate, "11.12.12")
                .With(x => x.Address, "Moscow")
                .Without(x => x.Issues)
                .With(x => x.Num, "Заседание 1111")
                .With(x => x.Status, MeetingStatus.Draft)
                .CreateMany(10).ToList();
            _issues = _fixture.Build<ModelIssue>()
                .With(x => x.CollegialBody, Guid.NewGuid())
                .With(x => x.IsInformational, true)
                .With(x => x.Description, "discriptionss")
                .With(x => x.Estimate, "estimate")
                .Without(x => x.ModelMember)
                .With(x => x.Title, "TestQuestion")
                .With(x => x.Status, IssueStatus.Modified)
                .Without(x => x.Meeting)
                .CreateMany<ModelIssue>(10).ToList();
            /*
            _issues.Add(_fixture.Build<ModelIssue>()
                .With(x => x.CollegialBody, Guid.NewGuid())
                .With(x => x.IsInformational, true)
                .With(x => x.Description, "discriptionss")
                .With(x => x.Estimate, "estimate")
                .Without(x => x.ModelMember)
                .With(x => x.Title, "TestQuestion")
                .With(x => x.Status, IssueStatus.Modified)
                .Without(x => x.Meeting)
                .Create());*/
            _members = _fixture.CreateMany<Guid>(20).ToList();

            foreach (var issue in _issues)
            {
                var meeting = _meetings[random.Next(_meetings.Count)];

                issue.Meeting = meeting;
                issue.MeetingId = meeting.Id;
            }

            foreach (var meeting in _meetings)
            {
                meeting.Issues = _issues.Where(x => x.MeetingId == meeting.Id).ToList();
            }

            _issueMembers = new List<ModelMember>();
            foreach (var member in _members)
            {
                var issuesForMember = _issues.RandomSubset();

                foreach (var issue in issuesForMember)
                {
                    _issueMembers.Add(new ModelMember
                    {
                        MemberId = member,
                        IssueId = issue.Id

                    });
                }
            }
            _mockDbContext = new Mock<SenatContext>();
            _mockDbContext.Setup(x => x.Issues).Returns(_issues);
            _mockDbContext.Setup(x => x.Meetings).Returns(_meetings);
            _mockDbContext.Setup(x => x.Members).Returns(_issueMembers);
            _fixture.Register(()=> _mockDbContext.Object);

        }

       
        [Test]
        public void IsDateNull_ReturnsMeetingOrderByIssueCountGreaterThanOne()
        {
            var member = _members.First();
            var query = _fixture.Create<SenatApi.SqlIssueMeetingRepository>();
            var res = query.GetMeetingQueryResults(member, null);
            foreach (var item in res)
            {
                Console.WriteLine(item.Issues);
                Assert.That(item.Issues, Is.GreaterThan(1));
            }
        }

        [Test]
        public void IsMemberNull_ReturnNullIssues()
        {
            var query = _fixture.Create<SenatApi.SqlIssueMeetingRepository>();
            var res = query.GetMeetingQueryResults(null, DateTime.MinValue);
            Assert.IsTrue(res.All(x => x.Issues == 0));
            foreach (var item in res)
            {
                Assert.That(item.Issues, Is.Null);
            }
        }

        [Test]
        public void IsMemberAndDateNull_ReturnNullIssues()
        {
            var query = _fixture.Create<SenatApi.SqlIssueMeetingRepository>();
            var res = query.GetMeetingQueryResults(null, null);
            foreach (var item in res)
                Assert.That(item.Issues, Is.Null);
        }
        

        [Test]
        public void GetMeetingQueryResults_ReturnsAllMemberMeetings()
        {
            var memberId = _members.First();
            var issueIds = _issueMembers
                .Where(x => x.MemberId == memberId).Select(x => x.IssueId);

            var meetingIds = _issues
                .Where(x => issueIds.Contains(x.Id) && x.MeetingId.HasValue && x.Meeting.Issues.Count > 1)
                .Select(x => x.MeetingId.Value).Distinct().OrderBy(x => x).ToList();

            var query = _fixture.Create<SqlIssueMeetingRepository>();
            var res = query.GetMeetingQueryResults(memberId, DateTime.MinValue).Select(x => x.MeetingId).OrderBy(x => x).ToList();

            Assert.IsTrue(Enumerable.SequenceEqual(meetingIds, res));
        }

        [Test]
        public void IsRightNum_ReturnTrue()
        {
            var query = _fixture.Create<SqlIssueMeetingRepository>();
            var res = query.GetMeetingQueryResults(Guid.NewGuid(), DateTime.Today);
            Assert.That(res, Is.Empty);
            foreach (var item in res)
            {
                Assert.That(item.Num, Does.Contain("заседание 1111").IgnoreCase);
            }
        }

        [Test]
        public void OrderedByDate_ReturnTrue()
        {
            var member = _members.First();
            var query = _fixture.Create<SenatApi.SqlIssueMeetingRepository>();
            var res = query.GetMeetingQueryResults(member, DateTime.MinValue);
            Assert.That(res, Is.Ordered.By("Date"));
        }
    }
}
