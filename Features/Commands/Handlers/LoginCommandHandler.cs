using AutoMapper;
using ChatApp.Data;
using ChatApp.Features.Commands.Requests;
using ChatApp.Models.DTOs;
using ChatApp.Models.DTOs.Validators;
using ChatApp.Models.EntityModels;
using ChatApp.Models.Responses;
using ChatApp.Services;
using ChatApp.Services.JwtServices;
using MediatR;

namespace ChatApp.Features.Commands.Handlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, CommonResponse<LoggedInUserDto>>
{

    private readonly UnitOfWork _unitOfWork = null!;
    private readonly JwtProvider _jwtProvider;
    private readonly PasswordHasher _passwordHasher;

    public LoginCommandHandler(UnitOfWork unitOfWork, JwtProvider jwtProvider, PasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _jwtProvider = jwtProvider;
        _passwordHasher = passwordHasher;
    }

    public async Task<CommonResponse<LoggedInUserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var validator = new LoginUserDtoValidator(_unitOfWork, _passwordHasher);
        var validationResult = await validator.ValidateAsync(request.LoginUserDto);
        if (validationResult.IsValid){
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(request.LoginUserDto.Email);
            var userToken = _jwtProvider.Generate(user);
            var loggedInUserDto = new LoggedInUserDto{
                UserId = user.Id,
                Token = userToken
            };

            return CommonResponse<LoggedInUserDto>.Success(loggedInUserDto);
        }
        var errorMessages = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
        return CommonResponse<LoggedInUserDto>.FailureWithError("User login failed because of the follwing error", errorMessages);
    }
    }
