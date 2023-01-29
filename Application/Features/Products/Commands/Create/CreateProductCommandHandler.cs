using Application.Features.Users.Commands.Create;
using Application.Services.ProductService;
using Application.Services.UserService;
using AutoMapper;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductService productService, IUserService userService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Product> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var user = await _userService.GetById(command.OwnerId);
            if (user != null)
            {
                Product product = _mapper.Map<Product>(command);
                return await _productService.Create(product);
            }
            return null;
        }

    }
}