using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestProject.Data;

namespace TestProject.Middlewares
{
    public class LastActiveTimeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LastActiveTimeMiddleware> _logger;
        private readonly ApplicationDbContext _dbContext;

        public LastActiveTimeMiddleware(RequestDelegate next, ILogger<LastActiveTimeMiddleware> logger, ApplicationDbContext dbContext)
        {
            _next = next;
            _logger = logger;
            _dbContext = dbContext;
        }
        public async Task InvokeAsync(HttpContext context, CancellationToken token)
        {
            try
            {
                if (context.User.Identity?.IsAuthenticated ?? false)
                {
                    var username = context.User.Identity.Name;
                    var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username, token);

                    if (user == null)
                    {
                        _logger.LogError($"Update last active time: user {username} not found, skip");
                    }
                    else
                    {
                        user.LastActiveTime = DateTime.UtcNow;
                        await _dbContext.SaveChangesAsync(token);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update last user active time, skip");
            }

            await _next(context);
        }
    }
}