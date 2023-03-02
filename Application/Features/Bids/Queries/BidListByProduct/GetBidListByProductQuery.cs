using Application.Features.Bids.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.EntityModels;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bids.Queries.BidListByProduct
{
    public record GetBidListByProductQuery : IRequest<GetListBidByProductQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetListBidByProductQueryHandler : IRequestHandler<GetBidListByProductQuery, GetListBidByProductQueryResponse>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetListBidByProductQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetListBidByProductQueryResponse> Handle(GetBidListByProductQuery request, CancellationToken cancellationToken)
        {
            var getListBidQueryResponse = new GetListBidByProductQueryResponse();

            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (product != null)
            {
                var listBids = await _context.Bids.AsQueryable().ProjectTo<BidDto>(_mapper.ConfigurationProvider).Where(b => b.ProductId == product.Id).ToListAsync(cancellationToken);

                getListBidQueryResponse.SuccessResponse(listBids);
            }
            else
            {
                getListBidQueryResponse.NotFoundResponse();
            }

            return getListBidQueryResponse;
        }
    }
}
// #??#
//                 var listBids = product.Bids;

// var listBids = product.Bids.AsQueryable().ProjectTo<BidDto>(_mapper.ConfigurationProvider).ToList();


// await _context.Entry(product).Collection(b => b.Bids.AsQueryable().ProjectTo<BidDto>(_mapper.ConfigurationProvider).ToList()).LoadAsync(cancellationToken);


//var listBids = product.Bids.AsQueryable().ProjectTo<BidDto>(_mapper.ConfigurationProvider).ToList();

// await _context.Entry(product).Collection(p => p.Bids).LoadAsync(cancellationToken);
// var listBids = product.Bids.Select(b => new BidDto
// {
//     BidPrice = b.BidPrice,
//     Date = b.Date,
//     UserId = b.UserId
// }).ToList();

//var listBids = await _context.Bids.AsQueryable().ProjectTo<BidDto>(_mapper.ConfigurationProvider).Where(b => b.ProductId == product.Id).ToListAsync(cancellationToken);
// Includes garbage generated by the worker function.
