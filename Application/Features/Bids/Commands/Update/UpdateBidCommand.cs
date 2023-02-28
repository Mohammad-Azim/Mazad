using Application.Features.Bids.Commands.Create;
using Application.Features.Bids.Dtos;
using Application.Helper.Profiles;
using MediatR;

namespace Application.Features.Bids.Commands.Update
{
    public record UpdateBidCommand : BidDto, IRequest<UpdateBidCommandResponse>, IMapFrom<CreateBidCommand>
    {
        public int Id { get; set; }
    }
}