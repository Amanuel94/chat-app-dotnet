using ChatApp.Models.DTOs;
using ChatApp.Models.Responses;
using MediatR;

namespace ChatApp.Features.Commands.Requests;

public class CreateUserCommand : IRequest<CommonResponse<UserDto>>
{
    public CreateUserDto CreateUserDto { get; set; } = null!;
}