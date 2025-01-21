using Rewardergg.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewardergg.Application.Interfaces
{
    public interface IAuthService
    {
        Task<OauthResponseDto> AuthenticateWithOauth(string code);

        Task CreateUserToken();
    }
}
