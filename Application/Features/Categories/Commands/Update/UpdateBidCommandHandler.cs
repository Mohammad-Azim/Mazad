using Application.Services.CategoryService;
using AutoMapper;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Category>
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<Category> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            Category category = _mapper.Map<Category>(command);
            var value = await _categoryService.GetById(category.Id);
            if (value != null)
                return await _categoryService.Update(category);
            return null;
        }


    }
}

