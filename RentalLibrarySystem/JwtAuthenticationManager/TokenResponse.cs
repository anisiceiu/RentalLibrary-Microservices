using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationManager
{
    public class TokenResponse
    {
        public string Username { get; set; }    
        public string AccessToken { get; set; }
        public DateTime ExpiresOnUtc { get; set; }
    }
}
