using ChatApp.Models.DTOs;
using ChatApp.Models.Responses;
using MediatR;

namespace ChatApp.Features.Commands.Requests;

public class LoginCommand : IRequest<CommonResponse<LoggedInUserDto>>
{
    public LoginUserDto LoginUserDto { get; set; } = null!;
}