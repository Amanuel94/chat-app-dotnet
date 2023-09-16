
using ChatApp.Models.Data;

namespace ChatApp.Data;

public class UnitOfWork
{
    private readonly ChatAppDbContext _dbContext;
    private  UserRepository _userRepository = null!;
    private  MessageRepository _messageRepository = null!;

    public UnitOfWork(ChatAppDbContext dbContext) => _dbContext = dbContext;

    public UserRepository UserRepository
    {
        get => _userRepository ??= new UserRepository(_dbContext);
    }

    public MessageRepository MessageRepository
    {
        get => _messageRepository ??= new MessageRepository(_dbContext);
    }
    public async Task<int> SaveAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}
