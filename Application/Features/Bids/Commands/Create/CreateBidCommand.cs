using Application.Features.Bids.Dtos;
using MediatR;
namespace Application.Features.Bids.Commands.Create
{
    public record CreateBidCommand : BidDto, IRequest<CreateBidCommandResponse> { }
}