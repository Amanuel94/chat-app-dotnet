
using FluentValidation;
using ChatApp.Models.DTOs;
using ChatApp.Data;
using ChatApp.Services;

namespace ChatApp.Models.DTOs.Validators;

public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
{
    private readonly UserRepository _userRepository;
    private readonly PasswordHasher _passwordHasher;
    public LoginUserDtoValidator(UnitOfWork unitOfWork, PasswordHasher passwordHasher)
    {
        _userRepository = unitOfWork.UserRepository;
        _passwordHasher = passwordHasher;

        RuleFor(dto => dto.Email)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(1, 30).WithMessage("{PropertyName} length should be between 1 and 30 characters.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MustAsync(async (email, token) => {
                return await _userRepository.EmailExistsAsync(email);
            }).WithMessage("Email does not exist");


        RuleFor(dto => new {dto.Email, dto.Password})
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MustAsync(async (dto, token) => {
                    string hashedPassword;
                    var user = await _userRepository.GetByEmailAsync(dto.Email);
                    hashedPassword = user!.Password;
                    return _passwordHasher.VerifyPassword(hashedPassword, dto.Password);
                }).WithMessage("Password does not match");
            
    }


}
