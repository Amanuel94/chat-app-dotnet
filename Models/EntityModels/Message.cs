
namespace ChatApp.Models.EntityModels;

public class Message : BaseEntity{
    public string Content { get; set;} = null!;
    public int UserId { get; set; }
    public virtual User User { get; set; } =  null!;
}