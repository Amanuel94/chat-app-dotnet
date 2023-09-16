namespace ChatApp.Models.DTOs;

public class UserDto{
    public int Id { get; set;}
    public string UserName = null!;
    public string Email = null!;
    public string Password = null!;
    public DateTime CreatedAt;
    public DateTime ModifiedAt;
}