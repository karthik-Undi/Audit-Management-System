using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repository
{
    public interface IAuthenticationManager
    {
        public string Authenticate(string email, string password);
        int GetUserid(string email);

    }
}
