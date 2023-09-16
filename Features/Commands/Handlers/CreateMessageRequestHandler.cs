using AutoMapper;
using ChatApp.Data;
using ChatApp.Features.Commands.Requests;
using ChatApp.Models.DTOs;
using ChatApp.Models.DTOs.Validators;
using ChatApp.Models.EntityModels;
using ChatApp.Models.Responses;
using MediatR;

namespace ChatApp.Features.Commands.Handlers;

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, CommonResponse<MessageDto>>
{

    private readonly UnitOfWork _unitOfWork = null!;
    private readonly IMapper _mapper;
    public CreateMessageCommandHandler(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<CommonResponse<MessageDto>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateMessageDtoValidator(_unitOfWork);
        var validationResult = await validator.ValidateAsync(request.CreateMessageDto);

        if (validationResult.IsValid){
            var message = _mapper.Map<Message>(request.CreateMessageDto);
            await _unitOfWork.MessageRepository.CreateAsync(message);
            int changesSaved = await _unitOfWork.SaveAsync();
            if(changesSaved == 0){
                return CommonResponse<MessageDto>.Failure("Message creation failed because of internal error");
            }
            return CommonResponse<MessageDto>.Success(_mapper.Map<MessageDto>(message));
        }
        var errorMessages = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
        return CommonResponse<MessageDto>.FailureWithError("Message creation failed because of the follwing errors", errorMessages);
    }
}