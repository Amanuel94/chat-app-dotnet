using ChatApp.Models.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Services;

public class ChatHub : Hub
{
    public Task SendMessage1(UserDto user, MessageDto message)
    {
        return Clients.All.SendAsync("ReceiveOne", user, message);
    }
}
