using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using SenatApi;

namespace SenatWebTest
{
    
    public static class ConfigMapper1
    {
        
        public static IMapper IssueModelMap()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ModelIssue, ModelIssueDto1> ()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.CollegialBody, act => act.MapFrom(src => src.CollegialBody))
                .ForMember(dest => dest.IsInformational, act => act.MapFrom(src => src.IsInformational))
                .ForMember(dest => dest.Title, act => act.MapFrom(src => src.Title))
                .ForMember(dest => dest.Meeting, act => act.MapFrom(src => src.MeetingId))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                .ForMember(dest => dest.Estimate, act => act.MapFrom(src => src.Estimate))
                .ForMember(dest => dest.Status, act => act.MapFrom(src => src.Status));
              
                

            });
            
            var mapper = config.CreateMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            return mapper;
        }
        
    }
}