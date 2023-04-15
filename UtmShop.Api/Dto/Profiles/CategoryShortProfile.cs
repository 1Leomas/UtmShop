using AutoMapper;

namespace UtmShop.Api.Dto.Profiles;

public class CategoryShortProfile : Profile
{
    public CategoryShortProfile()
    {
        CreateMap<Category, CategoryShortDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.ItemsCount, opt => opt.MapFrom(src => src.Products.Count));
    }
}