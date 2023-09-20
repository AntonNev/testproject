using System.Threading;
using System.Threading.Tasks;
using TestProject.Contracts.Dto;

namespace TestProject.Application.Services
{
    public interface IUserService
    {
        Task AddUserAsync(UserDto user, CancellationToken token);
    }
}