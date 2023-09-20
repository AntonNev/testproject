using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestProject.Application.Services;
using TestProject.Contracts.Dto;

namespace TestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task Add(UserDto user, CancellationToken cancellationToken)
        {
            await _userService.AddUserAsync(user, cancellationToken);
        }
    }
}