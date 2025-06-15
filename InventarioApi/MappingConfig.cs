using AutoMapper;
using InventarioApi.Models;
using InventarioApi.Models.Dto;

namespace InventarioApi
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                // Categoría
                config.CreateMap<CategoryDto, Category>();
                config.CreateMap<Category, CategoryDto>();

                // Producto
                config.CreateMap<ProductDto, Product>().ForMember(dest => dest.Category, opt => opt.Ignore());
                config.CreateMap<Product, ProductDto>()
                    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

                // ProductoCreateDto -> Product (ignorar propiedad de navegación)
                config.CreateMap<ProductCreateDto, Product>()
                    .ForMember(dest => dest.Category, opt => opt.Ignore());

                // ProductoCreateDto -> Product (ignorar propiedad de navegación)
                config.CreateMap<ProductUpdateDto, Product>()
                    .ForMember(dest => dest.Category, opt => opt.Ignore());
            });

            return mappingConfig;
        }
    }
}
