namespace ChatApp.Models.DTOs;

public class UserDto{
    public int Id { get; set;}
    public string? UserName{ get; set;}
    public string? Email{ get; set;}
    public DateTime CreatedAt{ get; set;}
    public DateTime ModifiedAt{ get; set;}
}