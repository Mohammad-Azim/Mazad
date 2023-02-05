using Application.Services.ProductService;
using AutoMapper;
using Domain.EntityModels;
using FluentValidation;
using MediatR;

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
    {
        private readonly IProductService _productService;
        private readonly IValidator<CreateProductCommand> _validator;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductService productService, IValidator<CreateProductCommand> validator, IMapper mapper)
        {
            _productService = productService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var createProductCommandResponse = new CreateProductCommandResponse();

            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            // #??# these two if statement repeated !!(Repeated code) 
            if (validationResult.Errors.Count > 0)
            {
                createProductCommandResponse.ErrorsResponse(validationResult.Errors);
            }

            if (createProductCommandResponse.Success)
            {
                Product product = _mapper.Map<Product>(command);
                product.EndTime = product.EndTime.ToUniversalTime();
                var data = await _productService.Create(product);
                createProductCommandResponse.SuccessResponse(data);
                // #??# createProductCommandResponse.message && try catch 
            }
            return createProductCommandResponse;
        }
    }
}