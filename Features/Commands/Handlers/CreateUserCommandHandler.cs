using AutoMapper;
using ChatApp.Data;
using ChatApp.Features.Commands.Requests;
using ChatApp.Models.DTOs;
using ChatApp.Models.DTOs.Validators;
using ChatApp.Models.EntityModels;
using ChatApp.Models.Responses;
using MediatR;

namespace ChatApp.Features.Commands.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CommonResponse<UserDto>>
{

    private readonly UnitOfWork _unitOfWork = null!;
    private readonly IMapper _mapper;
    public CreateUserCommandHandler(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<CommonResponse<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateUserDtoValidator(_unitOfWork);
        var validationResult = await validator.ValidateAsync(request.CreateUserDto);
        if (validationResult.IsValid){
            var user = _mapper.Map<User>(request.CreateUserDto);
            await _unitOfWork.UserRepository.CreateAsync(user);
            int changesSaved = await _unitOfWork.SaveAsync();
            if(changesSaved == 0){
                return CommonResponse<UserDto>.Failure("User creation failed because of internal error");
            }
            return CommonResponse<UserDto>.Success(_mapper.Map<UserDto>(user));
        }
        var errorMessages = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
        return CommonResponse<UserDto>.FailureWithError("User creation failed because of the follwing error", errorMessages);
    }
}