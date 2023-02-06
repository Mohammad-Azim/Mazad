using Application.Features.Categories.Commands.Create;
using Application.Services.CategoryService;
using AutoMapper;
using Domain.EntityModels;
using FluentValidation;
using MediatR;

namespace Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryCommandResponse>
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<CreateCategoryCommand> _validator;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryService categoryService, IValidator<CreateCategoryCommand> validator, IMapper mapper)
        {
            _categoryService = categoryService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var updateCategoryCommandResponse = new UpdateCategoryCommandResponse();
            CreateCategoryCommand category = _mapper.Map<CreateCategoryCommand>(command);

            var validationResult = await _validator.ValidateAsync(category, cancellationToken);
            var categoryById = await _categoryService.GetById(command.Id);

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
                Category result = _mapper.Map<Category>(command);
                var data = await _categoryService.Create(result);
                updateCategoryCommandResponse.SuccessResponse(data);
            }
            return updateCategoryCommandResponse;
        }
    }
}
