using Application.Features.Bids.Dtos;
using Domain.EntityModels;
using MediatR;

namespace Application.Features.Bids.Commands.Update
{
    public record UpdateBidCommand : BidDto, IRequest<UpdateBidCommandResponse>
    {
        public int Id { get; set; }
    }
}