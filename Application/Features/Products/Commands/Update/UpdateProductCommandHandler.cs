using Application.Services.ProductService;
using AutoMapper;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Products.Commands.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<Product> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            Product Product = _mapper.Map<Product>(command);
            var value = await _productService.GetById(Product.Id);
            if (value != null)
                return await _productService.Update(Product);
            return null;
        }
    }
}

