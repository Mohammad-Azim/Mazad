using Application.Helper.Response;
using Application.Helper.ServiceExtensions;
using Domain.EntityModels;

namespace Application.Features.Bids.Commands.Delete
{
    public interface IDeleteBidCommandResponse : IBaseResponse<Bid> { }

    [ScopedRegistration]
    public class DeleteBidCommandResponse : BaseResponse<Bid>, IDeleteBidCommandResponse { }
}