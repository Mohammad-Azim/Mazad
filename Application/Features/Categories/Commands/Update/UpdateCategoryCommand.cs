using Application.Features.Categories.Dtos;
using MediatR;
using Application.Features.Categories.Commands.Create;
using Application.Services.CategoryService;
using AutoMapper;
using Domain.EntityModels;
using FluentValidation;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Categories.Commands.Update
{
    public record UpdateCategoryCommand : CategoryDto, IRequest<UpdateCategoryCommandResponse>
    {
        public int Id { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryCommandResponse>
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidator<CreateCategoryCommand> _validator;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ApplicationDbContext context, IValidator<CreateCategoryCommand> validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var updateCategoryCommandResponse = new UpdateCategoryCommandResponse();
            CreateCategoryCommand category = _mapper.Map<CreateCategoryCommand>(command);

            var validationResult = await _validator.ValidateAsync(category, cancellationToken);
            var categoryById = await _context.Categories.SingleAsync(a => a.Id == command.Id, cancellationToken);


            if (categoryById == null)
            {
                return (UpdateCategoryCommandResponse)updateCategoryCommandResponse.NotFoundResponse("Product Not Found");
            }
            if (validationResult.Errors.Count > 0)
            {
                updateCategoryCommandResponse.ErrorsResponse(validationResult.Errors);
            }
            if (updateCategoryCommandResponse.Success)
            {
                var data = _context.Categories.Update(categoryById);
                await _context.SaveChangesAsync(cancellationToken);
                updateCategoryCommandResponse.SuccessResponse(data.Entity);
            }
            return updateCategoryCommandResponse;
        }
    }
}