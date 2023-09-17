using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChatApp.Models.DTOs;
using ChatApp.Features.Commands.Requests;
using ChatApp.Features.Queries.Requests;
using ChatApp.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/chat-app")]
    public class ChatAppController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserService _userService;
        private readonly IHubContext<ChatHub> _hubContext;
        public ChatAppController(IMediator mediator, UserService userService, IHubContext<ChatHub> hubContext)
        {
            _mediator = mediator;
            _userService = userService;
            _hubContext = hubContext;
        }


        [HttpPost("message")]
        public async Task<IActionResult> CreateMessage([FromBody] string content)
        {

            var currentUserId = _userService.GetUserId();

            var createMessageCommand = new CreateMessageCommand
            {
                CreateMessageDto = new CreateMessageDto
                {
                    Content = content,
                    UserId = currentUserId
                }
            };

            var response = await _mediator.Send(createMessageCommand);
            if (response.IsSuccess)
                return CreatedAtAction(nameof(GetMessage), new { id = response.Value.Id }, response);
            return BadRequest(response);
        }

        // [HttpPost("user")]
        // public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        // {

        //     var createUserCommand = new CreateUserCommand{
        //         CreateUserDto = createUserDto
        //     };

        //     var response = await _mediator.Send(createUserCommand);
        //     if(response.IsSuccess)
        //         return CreatedAtAction(nameof(GetUser), new { id = response.Value.Id }, response);
        //     return BadRequest(response);
        // }

        [HttpGet("message")]
        public async Task<IActionResult> GetMessages()
        {

            var getAllMessagesQuery = new GetAllMessagesQuery();

            var response = await _mediator.Send(getAllMessagesQuery);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("message/{id}")]
        public async Task<IActionResult> GetMessage(int id)
        {

            var getMessageByIdQuery = new GetMessageByIdQuery
            {
                MessageId = id
            };

            var response = await _mediator.Send(getMessageByIdQuery);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }


        [HttpGet("user")]
        public async Task<IActionResult> GetUsers()
        {

            var getAllUsersQuery = new GetAllUsersQuery();

            var response = await _mediator.Send(getAllUsersQuery);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }

        [AllowAnonymous]
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {

            var getMessageByIdQuery = new GetUserByIdQuery
            {
                UserId = id
            };

            var response = await _mediator.Send(getMessageByIdQuery);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }

    }
}
