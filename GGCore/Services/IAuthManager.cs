using GGCore.DTOs;
using System.Threading.Tasks;

namespace GGCore.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO userDTO);
        Task<string> CreateToken();
    }
}
