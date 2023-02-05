using Application.Features.Products.Commands.Create;
using Application.Services.CategoryService;
using AutoMapper;
using Domain.EntityModels;
using FluentValidation;
using MediatR;

namespace Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<CreateCategoryCommand> _validator;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryService categoryService, IMapper mapper, IValidator<CreateCategoryCommand> validator)
        {
            _categoryService = categoryService;
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
                var data = await _categoryService.Create(category);
                createCategoryCommandResponse.SuccessResponse(data);
            }
            return createCategoryCommandResponse;


        }
    }
}