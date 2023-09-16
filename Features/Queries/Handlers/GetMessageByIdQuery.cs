using AutoMapper;
using ChatApp.Data;
using ChatApp.Features.Queries.Requests;
using ChatApp.Models.DTOs;
using ChatApp.Models.Responses;
using MediatR;

namespace ChatApp.Features.Queries.Handlers;

public class GetMessageByIdQueryHandler : IRequestHandler<GetMessageByIdQuery, CommonResponse<MessageDto>>
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetMessageByIdQueryHandler(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<CommonResponse<MessageDto>> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
    {
        return CommonResponse<MessageDto>.Success(_mapper.Map<MessageDto>(await _unitOfWork.MessageRepository.GetAsync(request.MessageId)));
    }
}