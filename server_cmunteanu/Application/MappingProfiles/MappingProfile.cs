using Application.Models;
using Application.Models.Request;
using Application.Models.Response;
using Application.Repositories.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreateModel, User>();
            CreateMap<User, UserModel>();

            CreateMap<ProductCreateModel, Product>();

            CreateMap<Product, ProductModel>();

            CreateMap<OrderCreateModel, Order>();
            CreateMap<OrderProductModel, OrderProduct>();
            CreateMap<OrderProduct, OrderProductModel>();
            CreateMap<Order, OrderResponseModel>()
                .ForMember(m => m.Products, x => x.MapFrom(e => e.OrdersProducts));

            CreateMap<Order, EmailConfirmationModel>()
                .ForMember(m => m.ReceiverEmail, x => x.MapFrom(e => e.PlacedBy.Email))
                .ForMember(m => m.ReceiverName, x => x.MapFrom(e => e.PlacedBy.Name))
                .ForMember(m => m.OrderNumber, x => x.MapFrom(e => e.Id))
                .ForMember(m => m.OrderDate, x => x.MapFrom(e => e.Date.ToString("dd.MM.yyyy")));
        }
    }
}
