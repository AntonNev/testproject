using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestProject.Contracts.Dto;
using TestProject.Data;
using TestProject.Data.Entities;

namespace TestProject.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<UserService> _logger;

        public UserService(ApplicationDbContext dbContext, ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task AddUserAsync(UserDto user, CancellationToken token)
        {
            if (user == null)
            {
                _logger.LogWarning($"Add user: user is null");
                throw new TestProjectException("User is null");
            }

            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == user.Username, token);

            if (existingUser != null)
            {
                _logger.LogWarning($"Add user: user {user.Username} already exists");
                throw new TestProjectException("User already exists");
            }

            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var dbUser = new User()
            {
                Username = user.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password, salt),
                LastActiveTime = DateTime.UtcNow
            };

            await _dbContext.AddAsync(dbUser, token);
            await _dbContext.SaveChangesAsync(token);
        }
    }
}