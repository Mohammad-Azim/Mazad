using Application.Helper.Response;
using Application.Helper.ServiceExtensions;
using Domain.EntityModels;

namespace Application.Features.Bids.Queries.GetWithEvents
{
    public interface IGetBidByIdQueryResponse : IBaseResponse<Bid> { }

    [ScopedRegistration]
    public class GetBidByIdQueryResponse : BaseResponse<Bid>, IGetBidByIdQueryResponse { }
}