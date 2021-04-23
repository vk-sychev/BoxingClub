using BoxingClub.BLL.DTO;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface IAuthenticationService
    {
        Task<SignInResultDTO> SignInAsync(UserDTO user);

        Task SignOutAsync();
    }
}
