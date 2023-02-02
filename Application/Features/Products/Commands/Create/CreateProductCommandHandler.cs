using Application.Services.CategoryService;
using Application.Services.ProductService;
using Application.Services.UserService;
using AutoMapper;
using Domain.Common.Response;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductService productService, IUserService userService, ICategoryService categoryService, IMapper mapper)
        {
            _productService = productService;
            _userService = userService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var createProductCommandResponse = new CreateProductCommandResponse();

            var validator = new CreateProductCommandValidator();
            var validationResult = validator.Validate(command);
            var user = await _userService.GetById(command.OwnerId);
            var category = await _categoryService.GetById(command.CategoryId);

            if (validationResult.Errors.Count > 0 || user == null || category == null)
            {
                createProductCommandResponse.Success = false;
                createProductCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createProductCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
                if (user == null)
                {
                    createProductCommandResponse.ValidationErrors.Add("User Not Found");
                }
                if (category == null)
                {
                    createProductCommandResponse.ValidationErrors.Add("Category Not Found");
                }
                createProductCommandResponse.StatusCode = CodeStatusEnum.UnprocessableEntity;
            }

            if (createProductCommandResponse.Success)
            {
                Product product = _mapper.Map<Product>(command);
                product.EndTime = product.EndTime.ToUniversalTime();
                createProductCommandResponse.Data = await _productService.Create(product);
                createProductCommandResponse.StatusCode = CodeStatusEnum.Ok;
                // #??# createProductCommandResponse.message && try catch 
            }
            return createProductCommandResponse;
        }
    }
}