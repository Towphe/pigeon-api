
using Microsoft.EntityFrameworkCore;
using repo;

namespace services.auth
{
    public class ServiceStatus {
        public ServiceStatus(bool isSuccess, string? detail = null, dynamic? data = null)
        {
            this.IsSuccess = isSuccess;
            this.Detail = detail;
            this.Data = data;
        }
        public bool IsSuccess {get; set;}
        public string? Detail {get; set;}
        public dynamic? Data {get; set;}
    }
    public interface IAuthService
    {
        public Task<ServiceStatus> CreateUser(string email, string userId);
    }

    public class AuthService : IAuthService
    {
        private readonly PigeonContext _dbContext;
        public AuthService(PigeonContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ServiceStatus> CreateUser(string email, string userId)
        {
            // search first
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == email);
            
            if (user != null)
            {
                // mark failed if user exists
                return new ServiceStatus(false, "ExistingUser");
            }

            var newUser = new User(){
                Username = email,
            };

            await _dbContext.Users.AddAsync(newUser);

            return new ServiceStatus(true);
        }
    }
}