using AutoMapper;
using Member.API.DTOs;
using Member.API.Entities;

namespace Member.API.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<MemberDetail, Common.SharedModels.Member>().ReverseMap();
            CreateMap<MemberDto, Common.SharedModels.Member>().ReverseMap();
            CreateMap<MemberDto, MemberDetail>().ReverseMap();
        }
        
    }
}
