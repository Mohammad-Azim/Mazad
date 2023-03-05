using Application.Helper.Response;
using Application.Helper.ServiceExtensions;
using Domain.EntityModels;

namespace Application.Features.Bids.Commands.Create
{
    public interface ICreateBidCommandResponse : IBaseResponse<Bid> { }

    [ScopedRegistration]
    public class CreateBidCommandResponse : BaseResponse<Bid>, ICreateBidCommandResponse { }
}