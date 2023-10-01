using eventz.Data;
using eventz.Models;
using eventz.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eventz.Repositories
{
    public class UserRepositorie : IUserRepositorie
    {
        private readonly UserDbContext _dbContext;
        public UserRepositorie(UserDbContext userDbContext) 
        {
            _dbContext = userDbContext;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }
        public async Task<bool> DataIsUnique(User user)
        {
            if (!await _dbContext.Users.AnyAsync(x => x.CPF == user.CPF) || !await _dbContext.Users.AnyAsync(x => x.CNPJ == user.CNPJ))
            {
                return true;
            }
            return false;
        }
        public async Task<User> GetUserById(Guid id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> Create(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> Delete(Guid id)
        {
            User userId = await GetUserById(id);

            if (userId == null)
            {
                throw new InvalidOperationException("User not found");
            }

            
            _dbContext.Users.Remove(userId);
            await _dbContext.SaveChangesAsync();

            return true;
        }


        public async Task<User> Update(User user, Guid id)
        {
            User userId =  await GetUserById(id);

            if(userId == null) 
            {
                throw new InvalidOperationException("User not found");
            }

            userId.FirstName = user.FirstName;
            userId.LastName = user.LastName;
            userId.Email = user.Email;
            userId.Username = user.Username;
            userId.UpdatedAt = DateTime.Now;

            _dbContext.Users.Update(userId);
            await _dbContext.SaveChangesAsync();

            return userId;

        }
    }
}
