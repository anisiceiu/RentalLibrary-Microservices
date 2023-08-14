using AutoMapper;
using Borrowing.API.Entities;

namespace Borrowing.API.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Request, Common.SharedModels.Request>().ReverseMap();
        }
    }
}
