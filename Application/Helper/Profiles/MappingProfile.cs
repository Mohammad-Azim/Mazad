using Application.Features.Bids.Commands.Create;
using Application.Features.Bids.Commands.Update;
using Application.Features.Bids.Dtos;
using Application.Features.Categories.Commands.Update;
using Application.Features.Categories.Dtos;
using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Dtos;
using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Dtos;
using AutoMapper;
using Domain.EntityModels;

namespace Application.Helper.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserCommand, User>().ReverseMap();
            CreateMap<UpdateUserCommand, User>().ReverseMap();
            CreateMap<UpdateUserCommand, UserDto>().ReverseMap();

            CreateMap<CreateProductCommand, Product>().ReverseMap();
            CreateMap<UpdateProductCommand, Product>().ReverseMap();
            CreateMap<UpdateProductCommand, ProductDto>().ReverseMap();

            CreateMap<CreateBidCommand, Bid>().ReverseMap();
            CreateMap<UpdateBidCommand, Bid>().ReverseMap();
            CreateMap<UpdateBidCommand, BidDto>().ReverseMap();

            CreateMap<UpdateCategoryCommand, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}