using ChatApp.Models.DTOs;
using ChatApp.Models.Responses;
using MediatR;

namespace ChatApp.Features.Queries.Requests;

public class GetAllMessagesQuery: IRequest<CommonResponse<List<MessageDto>>>{

}