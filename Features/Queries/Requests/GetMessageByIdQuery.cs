using ChatApp.Models.DTOs;
using ChatApp.Models.Responses;
using MediatR;

namespace ChatApp.Features.Queries.Requests;

public class GetMessageByIdQuery : IRequest<CommonResponse<MessageDto>>
{
    public int MessageId { get; set; }
}