using System.Threading;
using System.Threading.Tasks;

namespace TestProject.Authentication.Services
{
    public interface IAuthChecker
    {
        Task<bool> CheckAuthAsync(string username, string password, CancellationToken token);
    }
}