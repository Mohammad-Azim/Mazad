using Application.Features.Bids.Dtos;
using Domain.EntityModels;
using MediatR;
namespace Application.Features.Bids.Commands.Create
{
    public record CreateBidCommand : BidDto, IRequest<Bid>
    {

    }
}