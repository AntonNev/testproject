using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestProject.Data;

namespace TestProject.Authentication.Services
{
    public class AuthChecker : IAuthChecker
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<AuthChecker> _logger;

        public AuthChecker(ApplicationDbContext dbContext, ILogger<AuthChecker> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<bool> CheckAuthAsync(string username, string password, CancellationToken token)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username, token);

            if (user == null)
            {
                _logger.LogWarning($"Check auth: username {username} not found");
                return false;
            }

            var checkResult = BCrypt.Net.BCrypt.Verify(password, user.Password);
            return checkResult;
        }
    }
}