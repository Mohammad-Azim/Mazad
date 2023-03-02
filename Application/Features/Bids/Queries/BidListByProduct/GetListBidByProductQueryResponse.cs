using Application.Features.Bids.Dtos;
using Application.Helper.Response;
using Domain.EntityModels;

namespace Application.Features.Bids.Queries.BidListByProduct
{
    public class GetListBidByProductQueryResponse : BaseResponse<ICollection<BidDto>> { }
}