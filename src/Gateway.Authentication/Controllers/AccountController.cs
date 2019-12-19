using Gateway.Authentication.Services;
using Gateway.Common.Commands;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Authentication.Controllers
{
    [ApiController]
    [Route("")]
    [EnableCors("AllowAnyOrigin")]
    public class AccountController : Controller
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IBusClient _busClient;

        public AccountController(IAuthenticateService authenticateService, IBusClient busClient)
        {
            _authenticateService = authenticateService;
            _busClient = busClient;
        }

        [HttpPost("login")]
        [EnableCors]
        public async Task<IActionResult> Login([FromBody]AuthenticateMerchant command)
            => Json(await _authenticateService.LoginAsync(command.Login, command.Password));

        [HttpPost("createuser")]
        public async Task CreateUser([FromBody]CreateUser command)
        {
                await _authenticateService.RegisterAsync(command.Login, command.Password,
                    command.Name, command.AcquiringBank);
        }
    }

}
