using ChatApp.Models.DTOs;
using ChatApp.Models.Responses;
using MediatR;

namespace ChatApp.Features.Commands.Requests;

public class CreateMessageCommand : IRequest<CommonResponse<MessageDto>>
{
    public CreateMessageDto CreateMessageDto { get; set; } = null!;
}