
namespace ChatApp.Models;

public class User:BaseEntity{
    public string UserName { get; set;} = null!;
    public string Email { get; set;} = null!;
    public string Password { get; set;} = null!;
    public virtual ICollection<Message> Messages { get; set; } = null!;
    
}