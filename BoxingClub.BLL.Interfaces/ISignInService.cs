using BoxingClub.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoxingClub.BLL.Interfaces
{
    public interface ISignInService
    {
        Task<SignInResultDTO> SignInAsync(UserDTO user);

        Task SignOutAsync();
    }
}
