using Application.Features.Products.Dtos;
using Application.Helper.Profiles;
using MediatR;
using AutoMapper;
using Domain.EntityModels;
using FluentValidation;
using Application.Context;

namespace Application.Features.Products.Commands.Create
{
    public record CreateProductCommand : ProductDto, IRequest<CreateProductCommandResponse>, IMapFrom<ProductDto> { }


    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidator<CreateProductCommand> _validator;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(ApplicationDbContext context, IValidator<CreateProductCommand> validator, IMapper mapper)
        {
            _context = context;
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
                await _context.Products.AddAsync(product, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                createProductCommandResponse.SuccessResponse(product);
            }
            return createProductCommandResponse;
        }
    }
}