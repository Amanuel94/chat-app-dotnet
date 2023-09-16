using ChatApp.Models.DTOs;
using ChatApp.Models.Responses;
using MediatR;

namespace ChatApp.Features.Queries.Requests;

public class GetUserByIdQuery : IRequest<CommonResponse<UserDto>>
{
    public int UserId { get; set; }
}