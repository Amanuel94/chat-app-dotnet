using AutoMapper;
using ChatApp.Data;
using ChatApp.Features.Queries.Requests;
using ChatApp.Models.DTOs;
using ChatApp.Models.Responses;
using MediatR;

namespace ChatApp.Features.Queries.Handlers;

public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, CommonResponse<List<MessageDto>>>
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetAllMessagesQueryHandler(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<CommonResponse<List<MessageDto>>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
    {
        return CommonResponse<List<MessageDto>>.Success(_mapper.Map<List<MessageDto>>(await _unitOfWork.MessageRepository.GetAsync()));
    }
}