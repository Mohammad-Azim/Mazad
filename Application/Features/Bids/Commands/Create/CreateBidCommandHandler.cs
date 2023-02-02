using Application.Services.BidService;
using Application.Services.ProductService;
using Application.Services.UserService;
using AutoMapper;
using Domain.EntityModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Features.Bids.Commands.Create
{
    public class CreateBidCommandHandler : IRequestHandler<CreateBidCommand, Bid>
    {
        private readonly IProductService _productService;
        private IValidator<CreateBidCommand> _validator;

        private readonly IUserService _userService;

        private readonly IBidService _bidService;

        private readonly IMapper _mapper;

        public ModelStateDictionary ModelState { get; set; }

        public CreateBidCommandHandler(IProductService productService, IUserService userService, IBidService bidService, IValidator<CreateBidCommand> validator, IMapper mapper)
        {
            _mapper = mapper;
            _productService = productService;
            _validator = validator;
            _userService = userService;
            _bidService = bidService;
        }

        public async Task<Bid> Handle(CreateBidCommand command, CancellationToken cancellationToken)
        {
            ValidationResult results = await _validator.ValidateAsync(command, cancellationToken);
            if (!results.IsValid)
            {

            }

            var validator = new CreateBidCommandValidation(_productService);
            var result = await validator.ValidateAsync(command, cancellationToken);
            if (!result.IsValid)
            {
                return null;
            }
            var user = await _userService.GetById(command.UserId);
            var product = await _productService.GetById(command.ProductId);
            if (user != null && product != null)
            {
                Bid bid = _mapper.Map<Bid>(command);
                bid.Date = DateTime.UtcNow;
                return await _bidService.Create(bid);
            }
            return null;
        }
    }
}