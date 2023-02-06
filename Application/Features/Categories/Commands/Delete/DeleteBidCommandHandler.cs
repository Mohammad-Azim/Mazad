using Application.Services.BidService;
using MediatR;

namespace Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryCommandResponse>
    {
        private readonly IBidService _bidService;

        public DeleteCategoryCommandHandler(IBidService ProductService)
        {
            _bidService = ProductService;
        }

        public async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var deleteCategoryCommandResponse = new DeleteCategoryCommandResponse();

            var result = await _bidService.Delete(request.Id);
            if (result == 0)
            {
                deleteCategoryCommandResponse.NotFoundResponse();
            }
            if (deleteCategoryCommandResponse.Success)
            {
                deleteCategoryCommandResponse.SuccessResponse();
            }
            return deleteCategoryCommandResponse;
        }
    }
}