using AutoMapper;
using ChatApp.Data;
using ChatApp.Features.Queries.Requests;
using ChatApp.Models.DTOs;
using ChatApp.Models.Responses;
using MediatR;

namespace ChatApp.Features.Queries.Handlers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, CommonResponse<List<UserDto>>>
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetAllUsersQueryHandler(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<CommonResponse<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return CommonResponse<List<UserDto>>.Success(_mapper.Map<List<UserDto>>(await _unitOfWork.UserRepository.GetAsync()));
    }
}
