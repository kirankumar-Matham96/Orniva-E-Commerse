using AutoMapper;
using OrnivaApi.DTOs.Category;
using OrnivaApi.Entities;

namespace OrnivaApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category
            CreateMap<Category, CategoryDto>();

            CreateMap<CreateCategoryDto, Category>();

            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}
