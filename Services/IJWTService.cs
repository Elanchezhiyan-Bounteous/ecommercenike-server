using ecommercenike_server.Models;

namespace ecommercenike_server.Services
{
     public interface IJwtService
    {
        string GenerateToken(User user);
    }
}