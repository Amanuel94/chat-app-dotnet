namespace ChatApp.Models.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    public override string ToString()
    {
        string delimiter = ";";
        return $"{Id}{delimiter}{UserName}{delimiter}{Email}";
    }

}