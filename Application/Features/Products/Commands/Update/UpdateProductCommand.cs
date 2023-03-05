using Application.Features.Products.Dtos;
using Application.Helper.Profiles;
using MediatR;
using Application.Features.Products.Commands.Create;
using AutoMapper;
using Domain.EntityModels;
using FluentValidation;
using Application.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Commands.Update
{
    public record UpdateProductCommand : ProductDto, IRequest<UpdateProductCommandResponse>, IMapFrom<ProductDto>
    {
        public int Id { get; set; }
    }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductCommandResponse>
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidator<CreateProductCommand> _validator;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(ApplicationDbContext context, IValidator<CreateProductCommand> validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var updateProductCommandResponse = new UpdateProductCommandResponse();
            CreateProductCommand product = _mapper.Map<CreateProductCommand>(command);
            var validationResult = await _validator.ValidateAsync(product, cancellationToken);
            var productById = await _context.Products.SingleOrDefaultAsync(p => p.Id == command.Id, cancellationToken);

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
                var data = _context.Products.Update(result);
                await _context.SaveChangesAsync(cancellationToken);
                updateProductCommandResponse.SuccessResponse(data.Entity);
            }
            return updateProductCommandResponse;
        }
    }
}