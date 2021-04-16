using BoxingClub.BLL.DTO;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface ISignInService
    {
        Task<SignInResultDTO> SignInAsync(UserDTO user);

        Task SignOutAsync();
    }
}
