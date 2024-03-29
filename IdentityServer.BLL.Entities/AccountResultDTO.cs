﻿using System.Collections.Generic;

namespace IdentityServer.BLL.Entities
{
    public class AccountResultDTO
    {
        public bool Succeeded { get; set; }
        public IEnumerable<AccountErrorDTO> Errors { get; set; }
    }
}
