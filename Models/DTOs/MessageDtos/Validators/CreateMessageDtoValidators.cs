
using FluentValidation;
using ChatApp.Models.DTOs;
using ChatApp.Data;

namespace ChatApp.Models.DTOs.Validators;

public class CreateMessageDtoValidator : AbstractValidator<CreateMessageDto>

{

    private readonly UserRepository _userRepository;
    public CreateMessageDtoValidator(UnitOfWork unitOfWork)
    {
        _userRepository =  unitOfWork.UserRepository;  

        RuleFor(dto => dto.Content)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(1, 3000).WithMessage("{PropertyName} length should be between 1 and 3000 characters.");
        RuleFor(dto => dto.UserId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .MustAsync(async (userId, token) => {
                var user = await _userRepository.GetAsync(userId);
                return user != null;
            } ).WithMessage("Invalid author id.");

        
    }


}
