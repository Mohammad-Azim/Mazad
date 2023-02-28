using Application.Features.Products.Commands.Create;
using Application.Services.ProductService;
using AutoMapper;
using Domain.EntityModels;
using FluentValidation;
using MediatR;

namespace Application.Features.Products.Commands.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductCommandResponse>
    {
        private readonly IProductService _productService;
        private readonly IValidator<CreateProductCommand> _validator;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductService productService, IValidator<CreateProductCommand> validator, IMapper mapper)
        {
            _productService = productService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var updateProductCommandResponse = new UpdateProductCommandResponse();
            CreateProductCommand product = _mapper.Map<CreateProductCommand>(command);
            Console.WriteLine("######################################3");
            Console.WriteLine(command.Id.ToString(), "asdfasdfasdfasdf");
            var validationResult = await _validator.ValidateAsync(product, cancellationToken);
            var productById = await _productService.GetById(command.Id);

            if (productById == null)
            {
                return (UpdateProductCommandResponse)updateProductCommandResponse.NotFoundResponse("Product Not Found");
            }
            if (validationResult.Errors.Count > 0)
            {
                updateProductCommandResponse.ErrorsResponse(validationResult.Errors);
            }

            if (updateProductCommandResponse.Success)
            {
                Product result = _mapper.Map<Product>(command);
                result.EndTime = result.EndTime.ToUniversalTime();
                var data = await _productService.Update(result);
                updateProductCommandResponse.SuccessResponse(data);
            }
            return updateProductCommandResponse;
        }
    }
}
