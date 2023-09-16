namespace ChatApp.Models.DTOs;

public class MessageDto
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public DateTime CreatedAt;
    public DateTime ModifiedAt;
}