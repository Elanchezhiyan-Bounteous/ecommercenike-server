using ecommercenike_server.Models;
using Supabase;
using BCrypt.Net;
using System.Threading.Tasks;
using ecommercenike_server.Contracts;

namespace ecommercenike_server.Services
{
    public class UserService : IUserService
    {
        private readonly Supabase.Client _supabaseClient;

        public UserService(Supabase.Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        public async Task<User> RegisterUser(RegisterRequest registerRequest)
        {
            var existingUser = await _supabaseClient
                .From<User>()
                .Where(u => u.Email == registerRequest.Email).Single();

            if (existingUser != null)
            {
                throw new Exception("User with this email already exists.");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

            var user = new User
            {
                Username = registerRequest.Username,
                Email = registerRequest.Email,
                PasswordHash = passwordHash
            };

            var insertResponse = await _supabaseClient.From<User>().Insert(user);

            return insertResponse.Models.FirstOrDefault();
        }

        public async Task<User> LoginUser(LoginRequest loginRequest)
        {
            var user = await _supabaseClient
                .From<User>()
                .Where(u => u.Email == loginRequest.Email)
                .Single();

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
            {
                throw new Exception("Invalid email or password.");
            }

            return user;
        }
    }
}
