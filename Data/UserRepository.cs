using ChatApp.Models.EntityModels;
using ChatApp.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data;
public class UserRepository
{
    private readonly ChatAppDbContext _dbContext;
    public UserRepository(ChatAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task CreateUser(User user){
        await _dbContext.Users.AddAsync(user);
    }


    public async Task<List<User>> GetAsync(){
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User?> GetAsync(int id){
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email == email);
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await _dbContext.Users.AnyAsync(user => user.UserName == username);
    }

}
