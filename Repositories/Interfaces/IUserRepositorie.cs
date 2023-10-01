using eventz.Models;

namespace eventz.Repositories.Interfaces
{
    public interface IUserRepositorie
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(Guid id);
        Task<User> Create(User user);
        Task<User> Update(User user, Guid id);
        Task<bool> Delete(Guid id);
        Task<bool> DataIsUnique(User user);

        Task<bool> AuthenticateAsync(string username, string password);
        Task<bool> UserExists(string username);
        public string GenerateToken(Guid id, string email);


    }
}
