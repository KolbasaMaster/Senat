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
    
    public class ConfigMapper
    {
        private static IMapper _mapper;
        
        public static void CreateModel()
        {
            var converter = new ConvertDictianory();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IssueDto, ModelIssue>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CollegialBody, act => act.MapFrom(src => src.collegialBody.Id))
                .ForMember(dest => dest.Title, act => act.ConvertUsing(converter))
                .ForMember(dest => dest.Description, act => act.ConvertUsing(converter));

                cfg.CreateMap<Identific, ModelMember>()
                   .ForMember(dest => dest.MemberId, act => act.MapFrom(src => src.Id))
                   .ForMember(dest => dest.IssueId, act => act.Ignore());
                

                cfg.CreateMap<MeetingDto, ModelMeeting>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Discriminator, act => act.MapFrom(src => src._discriminator))
                .ForMember(dest => dest.CollegialBody, act => act.MapFrom(src => src.collegialBody.Id))
                .ForMember(dest => dest.Address, act => act.ConvertUsing(converter))
                .ForMember(dest => dest.Issues, act => act.Ignore());
            });
       
            _mapper = config.CreateMapper();
        }
        public static Tuple<List<ModelIssue>, List<ModelMember>>  CreateIssuesAndMembers(IssueMeeting issueAndMeeting)
        {
            var issueModelList = new List<ModelIssue>();
            var memberModelList = new List<ModelMember>();
            foreach (var issue in issueAndMeeting.issues)
            {
                var issueToAdd = _mapper.Map<ModelIssue>(issue);
                issueModelList.Add(issueToAdd);

                foreach (var member in issue.invitees)
                {
                    var memberToAdd = _mapper.Map<ModelMember>(member);
                    memberToAdd.IssueId = issueToAdd.Id;
                    memberModelList.Add(memberToAdd);
                }
            }
            return new Tuple<List<ModelIssue>, List<ModelMember>>(issueModelList, memberModelList) ;
        }
       

        public static List<ModelMeeting> CreateMeeting(IssueMeeting issueAndMeeting)
        {
            var meetingModelList = new List<ModelMeeting>();
            foreach (var meeting in issueAndMeeting.meetings)
            {
                var meetings = _mapper.Map<ModelMeeting>(meeting);
                meetingModelList.Add(meetings);
            }
            return meetingModelList;
        }
    }
}
