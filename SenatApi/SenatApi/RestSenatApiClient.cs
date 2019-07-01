using System;
using System.Linq;
using RestSharp;
using NLog;
using System.Net;
using System.Collections.Generic;

namespace SenatApi
{

    public class RestSenatApiClient_
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        const string authCookie = "senat-auth";
        readonly string _baseUrl;
        readonly RestClient _restClient;
        readonly RestResponseCookie _restResponseCookie;
        LoginDto login = new LoginDto();


        public RestSenatApiClient_(string baseUrl)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            _baseUrl = baseUrl;
            _restClient = new RestClient(_baseUrl);
            var request1 = new RestRequest("api/account/login", Method.POST);
            request1.AddJsonBody(new LoginDto());
            var response = _restClient.Post<Identific>(request1);
            _restResponseCookie = response.Cookies.First((x) => x.Name == authCookie);
        }



        public IssueMultilingualDto GetIssue(Guid id)
        {
            var request = new RestRequest($"/api/v2.0/issues/{id}", Method.GET);
            return SendRequest<IssueMultilingualDto>(request);
        }

        public MeetingDto GetMeeting(Guid id)
        {
            var request = new RestRequest($"/api/v2.0/meetings/{id}", Method.GET);
            return SendRequest<MeetingDto>(request);
        }

        public List<PageOfIssueVersionIssuesListItemDto> GetListOfIssues()
        {
            var request = new RestRequest($"/api/v2.0/issues/", Method.GET);
            return SendRequest<List<PageOfIssueVersionIssuesListItemDto>>(request);
        }


        public Guid CreateIssue(IssueDto issue)
        {
            var request = new RestRequest("api/v2.0/issues", Method.POST, DataFormat.Json);
            request.AddJsonBody(issue);           
            var issueVersionId = SendRequest<Identific>(request).Id;
            //// we need issueId not issueVersionId to create meeting
            var getIssueIdRequest = new RestRequest($"api/v1.0/issueversions/{issueVersionId}", Method.GET);            
            return SendRequest<IssueVersionShort>(getIssueIdRequest).Issue.Id;
        }

        public void IssueToPrepeared(Guid issueId)
        {
            var request = new RestRequest($"api/v2.0/issues/{issueId}/status/ToPrepared", Method.POST);
            request.AddJsonBody(new StatusDto(false, false));
            SendRequest<Identific>(request);
        }

        public List<PageOfMeetingLocalizedDto> GetListOfMeetings()
        {
            var request = new RestRequest("/api/v2.0/meetings", Method.GET);
            return SendRequest<List<PageOfMeetingLocalizedDto>>(request);
        }

        public Guid CreateMeeting(CreateMeetingDto meeting)
        {
            var request = new RestRequest("api/v2.0/meetings", Method.POST, DataFormat.Json);
            request.AddJsonBody(meeting);

            return SendRequest<Identific>(request).Id;
        }

        public T SendRequest<T>(RestRequest request) where T : class, new()
        {
            request.AddCookie(_restResponseCookie.Name, _restResponseCookie.Value);
            var response = _restClient.Execute<T>(request);
            logger.Trace(response.Content);                 
            return response.Data;
        }
      
    }
  
}
    