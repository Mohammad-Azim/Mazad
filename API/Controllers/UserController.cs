
using Application.Features.Users.Commands.Create;
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

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        // [HttpGet]

        // public async Task<List<User>> GetStudentListAsync()
        // {


        //     var studentDetails = await mediator.Send(new GetStudentListQuery());

        //     return studentDetails;
        // }

        // [HttpGet("studentId")]
        // public async Task<User> GetStudentByIdAsync(int studentId)
        // {
        //     var studentDetails = await mediator.Send(new GetStudentByIdQuery() { Id = studentId });

        //     return studentDetails;
        // }

        [HttpPost]
        public async Task<ActionResult<User>> AddStudentAsync([FromBody] CreateUserCommand user)
        {

            var userDetail = await mediator.Send(user);
            return Ok(userDetail);
        }

        // [HttpPut]
        // public async Task<int> UpdateStudentAsync(User studentDetails)
        // {
        //     var isStudentDetailUpdated = await mediator.Send(new UpdateStudentCommand(
        //        studentDetails.Id,
        //        studentDetails.StudentName,
        //        studentDetails.StudentEmail,
        //        studentDetails.StudentAddress,
        //        studentDetails.StudentAge));
        //     return isStudentDetailUpdated;
        // }

        // [HttpDelete]
        // public async Task<int> DeleteStudentAsync(int Id)
        // {
        //     return await mediator.Send(new DeleteStudentCommand() { Id = Id });
        // }

    }
}