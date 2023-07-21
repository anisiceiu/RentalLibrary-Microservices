using AutoMapper;
using Catalog.API.DTOs;
using Catalog.API.Entities;

namespace Catalog.API.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<BookDto,Book>().ReverseMap().ForMember(dest=> dest.CategoryName,
                opts=> opts.MapFrom(src=> src.Category.Name));
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Binding, BindingDto>().ReverseMap();
        }
    }
}
