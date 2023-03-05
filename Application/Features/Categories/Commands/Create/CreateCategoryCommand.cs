using Application.Features.Categories.Dtos;
using MediatR;
using AutoMapper;
using Domain.EntityModels;
using FluentValidation;
using Application.Context;

namespace Application.Features.Categories.Commands.Create
{
    public record CreateCategoryCommand : CategoryDto, IRequest<CreateCategoryCommandResponse> { }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidator<CreateCategoryCommand> _validator;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ApplicationDbContext context, IMapper mapper, IValidator<CreateCategoryCommand> validator)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var createCategoryCommandResponse = new CreateCategoryCommandResponse();
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (validationResult.Errors.Count > 0)
            {
                createCategoryCommandResponse.ErrorsResponse(validationResult.Errors);
            }

            if (createCategoryCommandResponse.Success)
            {
                Category category = _mapper.Map<Category>(command);
                var data = await _context.Categories.AddAsync(category, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                createCategoryCommandResponse.SuccessResponse(data.Entity);
            }
            return createCategoryCommandResponse;
        }
    }
}