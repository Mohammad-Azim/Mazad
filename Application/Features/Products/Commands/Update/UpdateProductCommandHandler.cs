using Application.Features.Products.Commands.Create;
using Application.Services.CategoryService;
using Application.Services.ProductService;
using Application.Services.UserService;
using AutoMapper;
using Domain.Common.Response;
using Domain.EntityModels;
using FluentValidation;
using MediatR;

namespace Application.Features.Products.Commands.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductCommandResponse>
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;


        public UpdateProductCommandHandler(IProductService productService, IUserService userService, ICategoryService categoryService, IMapper mapper)
        {
            _productService = productService;
            _userService = userService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var updateProductCommandResponse = new UpdateProductCommandResponse();
            var validator = new CreateProductCommandValidator(); // #??# should create a UpdateProductCommandValidator
            CreateProductCommand product = _mapper.Map<CreateProductCommand>(command);

            var validationResult = validator.Validate(product);
            var productById = await _productService.GetById(command.Id);
            var user = await _userService.GetById(command.OwnerId);
            var category = await _categoryService.GetById(command.CategoryId);

            if (validationResult.Errors.Count > 0 || user == null || category == null || productById == null)
            {
                updateProductCommandResponse.Success = false;
                updateProductCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    updateProductCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
                if (user == null)
                {
                    updateProductCommandResponse.ValidationErrors.Add("User Not Found");
                }
                if (category == null)
                {
                    updateProductCommandResponse.ValidationErrors.Add("Category Not Found");
                }
                if (productById == null)
                {
                    updateProductCommandResponse.ValidationErrors.Add("Product Not Found");
                }
                updateProductCommandResponse.StatusCode = CodeStatusEnum.UnprocessableEntity;
            }

            if (updateProductCommandResponse.Success)
            {
                Product result = _mapper.Map<Product>(command);
                result.EndTime = result.EndTime.ToUniversalTime();
                updateProductCommandResponse.Data = await _productService.Create(result);
                updateProductCommandResponse.StatusCode = CodeStatusEnum.Ok;
            }
            return updateProductCommandResponse;
        }
    }
}
