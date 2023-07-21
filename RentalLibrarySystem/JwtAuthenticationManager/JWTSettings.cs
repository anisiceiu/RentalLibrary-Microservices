using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationManager
{
    public static class JWTSettings
    {
        public static readonly string Secret = "K1QuWkRD1Q7f9903zt7Xl62mpQCQcQ9KcZ2nprdPzD76Ic4icEnazkH5q0fVlPlrtsu0ESlCkLBszX6g";
        public static readonly string Issuer = "https://www.rentallibrarysystem.com";
        public static readonly string Audience = "https://www.rentallibrarysystem.com";
        public static readonly int TokenTimeoutMinutes = 1400;
    }
}
