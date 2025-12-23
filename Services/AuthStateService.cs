using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookClient.Services
{
    public class AuthStateService
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }

        public bool IsAuthenticated => !string.IsNullOrEmpty(Token);

        public void Logout()
        {
            Token = null;
            Username = null;
            Role = null;
        }
    }
}
