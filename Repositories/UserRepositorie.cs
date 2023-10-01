using eventz.Data;
using eventz.Models;
using eventz.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eventz.Repositories
{
    public class UserRepositorie : IUserRepositorie
    {
        private readonly UserDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public UserRepositorie(UserDbContext userDbContext, IConfiguration configuration) 
        {
            _dbContext = userDbContext;
            _configuration = configuration;
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

        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null) return false;

            if (user.Password != password) return false;

            return true;
        }

        public string GenerateToken(Guid id, string email)
        {
            var claims = new[]
            {
                new Claim("id",id.ToString()),
                new Claim("email",email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            var privateKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_configuration["jwt:SecretKey"]));

            var credentials = new SigningCredentials
                (privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> UserExists(string username)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null) return false;

            return true;
        }
    }
}
