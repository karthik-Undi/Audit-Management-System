using AuthorizationAPI.Model;
using AuthorizationAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationManager manager;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(LoginController));
        Userdetails _user = new Userdetails();
        public LoginController(IAuthenticationManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        public string Get()
        {
            return "Hello";
        }

        [AllowAnonymous]
        [HttpPost("AuthenicateUser")]
        public IActionResult AuthenticateUser([FromBody] Userdetails details)
        {
            _log4net.Info("Http Authentication Login request Initiated");
            var token = manager.Authenticate(details.Email, details.Password);
            if (token == null)
                return Unauthorized();
            TokenAndUserID tokenAndUserID = new TokenAndUserID
            {
                UserId = manager.GetUserid(details.Email),
                Token = token
            };
            return Ok(tokenAndUserID);
        }

    }
}
