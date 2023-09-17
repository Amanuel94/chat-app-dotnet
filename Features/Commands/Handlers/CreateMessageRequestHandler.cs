using AutoMapper;
using ChatApp.Data;
using ChatApp.Features.Commands.Requests;
using ChatApp.Models.DTOs;
using ChatApp.Models.DTOs.Validators;
using ChatApp.Models.EntityModels;
using ChatApp.Models.Responses;
using ChatApp.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Features.Commands.Handlers;

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, CommonResponse<MessageDto>>
{

    private readonly UnitOfWork _unitOfWork = null!;
    private readonly IMapper _mapper;
    private readonly IHubContext<ChatHub> _hubContext;
    public CreateMessageCommandHandler(UnitOfWork unitOfWork, IMapper mapper, IHubContext<ChatHub> hubContext)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _hubContext = hubContext;
        
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
            var user = _unitOfWork.UserRepository.GetAsync(request.CreateMessageDto.UserId);
            var userDto = _mapper.Map<UserDto>(user);
            var messageDto = _mapper.Map<MessageDto>(message);

            await _hubContext.Clients.All.SendAsync(userDto.ToString(), messageDto);
            return CommonResponse<MessageDto>.Success(_mapper.Map<MessageDto>(message));
        }
        var errorMessages = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
        return CommonResponse<MessageDto>.FailureWithError("Message creation failed because of the follwing errors", errorMessages);
    }
}