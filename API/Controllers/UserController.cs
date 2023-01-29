using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Dtos;
using Application.Features.Users.Queries.GetList;
using Application.Features.Users.Queries.GetWithEvents;
using AutoMapper;
using Domain.EntityModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this._mapper = mapper;
        }


        [HttpGet]
        public async Task<List<User>> GetUserListAsync()
        {
            var userDetails = await mediator.Send(new GetUserListQuery());
            return userDetails;
        }

        [HttpGet("userId")]
        public async Task<ActionResult<User>> GetUserByIdAsync(int userId)
        {
            var value = await mediator.Send(new GetUserByIdQuery() { Id = userId });
            return (value != null ? Ok(value) : NotFound());
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUserAsync([FromBody] CreateUserCommand user)
        {
            var value = await mediator.Send(user);
            return (value != null ? Ok(value) : BadRequest());
        }

        [HttpPut("userId")]
        public async Task<ActionResult<User>> UpdateUserAsync([FromBody] UserDto userDto, int userId)
        {
            UpdateUserCommand user = _mapper.Map<UpdateUserCommand>(userDto);
            user.Id = userId;
            var value = await mediator.Send(user);
            return (value != null ? Ok(value) : NotFound(user));
        }

        [HttpDelete("userId")]
        public async Task<ActionResult> DeleteUserAsync(int userId)
        {
            var value = await mediator.Send(new DeleteUserCommand() { Id = userId });
            return (value != 0 ? Ok() : NotFound());
        }

    }
}