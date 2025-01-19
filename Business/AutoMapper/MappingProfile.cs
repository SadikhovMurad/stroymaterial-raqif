using AutoMapper;
using Entity.Concrete;
using Entity.DtoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryWithSubcategoriesDto, Category>();
            CreateMap<Category, CategoryWithSubcategoriesDto>();

            CreateMap<SubcategoryDto, SubCategory>();
            CreateMap<SubCategory, SubcategoryDto>();
            CreateMap<SubcategoryWithCategoryDto, SubCategory>();
            CreateMap<SubCategory, SubcategoryWithCategoryDto>();

            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductForListDto, Product>();
            CreateMap<Product, ProductForListDto>();

            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderForListDto, Order>();
            CreateMap<Order, OrderForListDto>();

            CreateMap<OrderItemDto, OrderItem>();
            CreateMap<OrderItem, OrderItemDto>();

            CreateMap<CartItemDto, CartItem>();
            CreateMap<CartItem, CartItemDto>();

            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, EmployeeDto>();

            CreateMap<OrderAssignmentDto, OrderAssignment>();
            CreateMap<OrderAssignment, OrderAssignmentDto>();

        }
    }
}
