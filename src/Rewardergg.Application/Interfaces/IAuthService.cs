using Rewardergg.Application.DTOs;
using Rewardergg.Domain;
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

        public string CreateUserToken(User user);

        public string GetSessionUser();
    }
}
