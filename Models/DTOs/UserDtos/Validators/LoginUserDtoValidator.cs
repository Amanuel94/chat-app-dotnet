
using FluentValidation;
using ChatApp.Models.DTOs;
using ChatApp.Data;

namespace ChatApp.Models.DTOs.Validators;

public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
{
    private readonly UserRepository _userRepository;
    public LoginUserDtoValidator(UnitOfWork unitOfWork)
    {
        _userRepository = unitOfWork.UserRepository;

        RuleFor(dto => dto.Email)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(1, 30).WithMessage("{PropertyName} length should be between 1 and 30 characters.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MustAsync(async (email, token) => {
                return await _userRepository.EmailExistsAsync(email);
            }).WithMessage("Email does not exist");


        RuleFor(dto => dto.Password)
                .NotEmpty().WithMessage("{PropertyName} is required.").Length(8, 20).WithMessage("{PropertyName} length should be between 8 and 20 characters.");
            
    }


}
