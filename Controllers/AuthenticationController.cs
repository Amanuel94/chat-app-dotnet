using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChatApp.Models.DTOs;
using ChatApp.Features.Commands.Requests;
using ChatApp.Features.Queries.Requests;

namespace ChatApp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto)
        {

            var loginCommand = new LoginCommand{
                LoginUserDto = loginDto
            };

            var response = await _mediator.Send(loginCommand);

            if(response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto)
        {

            var createUserCommand = new CreateUserCommand{
                CreateUserDto = createUserDto
            };

            var response = await _mediator.Send(createUserCommand);
            if(response.IsSuccess)
                return CreatedAtAction(nameof(GetUser), new { id = response.Value.Id }, response);
            return BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {

            var getMessageByIdQuery = new GetUserByIdQuery{
                UserId=id
            };

            var response = await _mediator.Send(getMessageByIdQuery);
            if(response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }

        

    }
}
