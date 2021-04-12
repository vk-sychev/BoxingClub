using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.DTO
{
    public class AccountResultDTO
    {
        public bool Succeeded { get; set; }
        public IEnumerable<AccountErrorDTO> Errors { get; set; }
    }
}
