using Application.Features.Bids.Dtos;
using Application.Helper.Response;
using Application.Helper.ServiceExtensions;

namespace Application.Features.Bids.Queries.BidListByProduct
{
    public interface IGetListBidByProductQueryResponse : IBaseResponse<ICollection<BidDto>> { }

    [ScopedRegistration]
    public class GetListBidByProductQueryResponse : BaseResponse<ICollection<BidDto>>, IGetListBidByProductQueryResponse { }
}