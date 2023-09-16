using ChatApp.Models.EntityModels;
using ChatApp.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data;
public class MessageRepository
{
    private readonly ChatAppDbContext _dbContext;
    public MessageRepository(ChatAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task CreateAsync(Message Message){
        await _dbContext.Messages.AddAsync(Message);
    }


    public async Task<List<Message>> GetAsync(){
        return await _dbContext.Messages.ToListAsync();
    }

    public async Task<Message?> GetAsync(int id){
        return await _dbContext.Messages.FirstOrDefaultAsync(m => m.Id == id);
    }

}
