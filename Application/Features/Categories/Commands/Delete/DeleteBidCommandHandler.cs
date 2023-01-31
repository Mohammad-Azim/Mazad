using Application.Services.BidService;
using AutoMapper;
using MediatR;

namespace Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, int>
    {
        private readonly IBidService _bidService;

        public DeleteCategoryCommandHandler(IBidService ProductService)
        {
            _bidService = ProductService;
        }

        public async Task<int> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _bidService.Delete(request.Id);
        }
    }
}