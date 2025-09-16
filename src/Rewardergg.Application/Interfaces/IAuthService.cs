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
        Task<OauthResponseDto> GetTokenWithAuthorizationCodeAsync(string code, bool isRefresh = false);

        public DateTime GetJwtExpirationTime();

        public string CreateUserToken(User user);

        public string GetSessionUser();
    }
}
