using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Categories.Commands.Delete
{
    public record DeleteCategoryCommand : IRequest<DeleteCategoryCommandResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryCommandResponse>
    {
        private readonly ApplicationDbContext _context;

        public DeleteCategoryCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var deleteCategoryCommandResponse = new DeleteCategoryCommandResponse();
            var category = await _context.Categories.SingleAsync(a => a.Id == request.Id, cancellationToken);

            if (category != null)
            {
                var result = _context.Categories.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);
                deleteCategoryCommandResponse.SuccessResponse();
            }
            else
            {
                deleteCategoryCommandResponse.NotFoundResponse();
            }
            return deleteCategoryCommandResponse;
        }
    }
}