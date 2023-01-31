using Application.Features.Categories.Dtos;
using Application.Services.CategoryService;
using AutoMapper;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CategoryDto, Category>
    {
        private readonly ICategoryService _categoryService;

        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryService categoryService, IMapper mapper)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public async Task<Category> Handle(CategoryDto command, CancellationToken cancellationToken)
        {
            Category category = _mapper.Map<Category>(command);

            return await _categoryService.Create(category);
        }


    }
}