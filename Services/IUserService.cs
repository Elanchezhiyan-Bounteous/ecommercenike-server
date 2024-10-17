using ecommercenike_server.Contracts;
using ecommercenike_server.Models;
using System.Threading.Tasks;

namespace ecommercenike_server.Services
{
    public interface IUserService
    {
        Task<User> RegisterUser(RegisterRequest registerRequest);
        Task<User> LoginUser(LoginRequest loginRequest);
    }
}
