using System;
using Moq;
using NUnit.Framework;
using AutoFixture;
using SenatApi;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using NewProject.Migrations;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


namespace SenatApi.UnitTests
{
    
    [TestFixture]
    public class UnitTest_QueryIssue
    {

        private Fixture _fixture;
        private Mock<SenatContext> _mockDbContext;
        private List<ModelMeeting> _meetings;
        private List <ModelIssue> _issues;

        [SetUp]
        public void Setup()
        {
            Random random = new Random();
            _fixture = new Fixture();

            _meetings = _fixture.Build<ModelMeeting>()
                .With(x=>x.Date, DateTime.Now)
                .With(x=>x.Discriminator, "discriminator")
                .With(x=>x.CollegialBody,Guid.NewGuid())
                .With(x=>x.AgendaDueDate, "11.11.11")
                .With(x=>x.MaterialsDueDate, "11.12.12")
                .With(x=>x.Address,"Moscow")
                .Without(x=>x.Issues)
                .With(x=>x.Num, "Заседание 1111")
                .With(x=>x.Status, MeetingStatus.Draft)
                .CreateMany(10).ToList();
            _issues = _fixture.Build<ModelIssue>()
                .With(x=>x.Id, Guid.NewGuid())
                .With(x=>x.CollegialBody, Guid.NewGuid())
                .With(x=>x.IsInformational, true)
                .With(x=>x.Description, "discriptionss")
                .With(x=>x.Estimate, "estimate")
                .Without(x=>x.ModelMember)
                .With(x=>x.Title, "TestQuestion")
                .With(x=>x.Status, IssueStatus.Modified)
                .Without(x=>x.Meeting )
                .CreateMany<ModelIssue>(12).ToList();
            foreach (var issue in _issues)
            {
                var meeting = _meetings[random.Next(_meetings.Count)];
             
                issue.Meeting = meeting;
            }
            
        
            _mockDbContext = new Mock<SenatContext>();
            _mockDbContext.Setup(x => x.Meetings).Returns(_meetings);
            _mockDbContext.Setup(x => x.Issues).Returns((_issues));
            _fixture.Register(() => _mockDbContext.Object);


        }

        [Test]
        public void CountQuery_IsEqual()
        {
            var query = _fixture.Create<SqlIssueMeetingRepository>();

            var res = query.GetIssueQueryResults();
            Assert.IsTrue(res.Count() == _issues.Count);
        }
        [Test]
        public void NotNullQuery_True()
        {
            var query = _fixture.Create<SqlIssueMeetingRepository>();
            var res = query.GetIssueQueryResults();
            Assert.IsNotNull(res);
        }

        [Test]
        public void Equal_issueTitle()
        {
            var issue = _issues.FirstOrDefault();
            var query = _fixture.Create<SqlIssueMeetingRepository>();
            var res = query.GetIssueQueryResults();
            var resIssue = res.FirstOrDefault();
            Assert.AreEqual(issue.Title, resIssue.Title);
        }
        
        
    }
}
