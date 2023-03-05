using Application.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Commands.Delete
{
    public record DeleteProductCommand : IRequest<DeleteProductCommandResponse>
    {
        public int Id { get; set; }
    }
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeleteProductCommandResponse>
    {
        private readonly ApplicationDbContext _context;

        public DeleteProductCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var deleteProductCommandResponse = new DeleteProductCommandResponse();

            var result = await _context.Products.SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (result == null)
            {
                deleteProductCommandResponse.NotFoundResponse();
            }
            if (deleteProductCommandResponse.Success)
            {
                _context.Products.Remove(result);
                await _context.SaveChangesAsync(cancellationToken);
                deleteProductCommandResponse.SuccessResponse();
            }
            return deleteProductCommandResponse;
        }
    }
}