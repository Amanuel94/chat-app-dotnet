
namespace ChatApp.Models;

public class Message : BaseEntity{
    public string Content { get; set;} = null!;
    public virtual User User { get; set; } =  null!;
}