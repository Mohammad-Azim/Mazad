using Application.Helper.Response;
using Application.Helper.ServiceExtensions;
using Domain.EntityModels;

namespace Application.Features.Bids.Commands.Update
{
    public interface IUpdateBidCommandResponse : IBaseResponse<Bid> { }

    [ScopedRegistration]
    public class UpdateBidCommandResponse : BaseResponse<Bid>, IUpdateBidCommandResponse { }
}