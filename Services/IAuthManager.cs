using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IAuthManager
    {
        public Task<bool> ValidateUser(LoginUserDTO userDTO);
        public Task<string> CreateToken();
    }
}
