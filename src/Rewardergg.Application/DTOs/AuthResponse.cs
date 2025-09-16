using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewardergg.Application.DTOs
{
    public class AuthResponse
    {
        /// <summary>
        /// The short-lived JWT your API issues (used for authorization in your app).
        /// </summary>
        public string Jwt { get; set; }

        /// <summary>
        /// The refresh token that the client can use to request a new JWT
        /// when the old one expires.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// When the JWT will expire (in UTC).
        /// Useful for frontend to know when to refresh.
        /// </summary>
        public DateTime ExpiresAt { get; set; }
    }

}
