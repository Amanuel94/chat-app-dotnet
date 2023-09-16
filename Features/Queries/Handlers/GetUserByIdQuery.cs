using AutoMapper;
using ChatApp.Data;
using ChatApp.Features.Queries.Requests;
using ChatApp.Models.DTOs;
using ChatApp.Models.Responses;
using MediatR;

namespace ChatApp.Features.Queries.Handlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, CommonResponse<UserDto>>
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetUserByIdQueryHandler(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<CommonResponse<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<UserDto>(await _unitOfWork.UserRepository.GetAsync(request.UserId));
        return CommonResponse<UserDto>.Success(user);
    }
}