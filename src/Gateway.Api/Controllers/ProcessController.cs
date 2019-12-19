using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gateway.Common.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Gateway.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProcessController : ControllerBase
    {
        protected ProcessController()
        {
        }

        private readonly IBusClient _busClient;

        public ProcessController(IBusClient busClient)
        {
            _busClient = busClient;
        }

        [HttpGet("hello")]
        public IActionResult Get() => Content("Hello from Process Controller API!");

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]ProcessPayment command)
        {
            command.PaymentId = Guid.NewGuid();
            command.UserId = Guid.Parse(User.Identity.Name);
            command.CreatedAt = DateTime.UtcNow;
            await _busClient.PublishAsync(command);

            var logCreated = new CreateLog(command.UserId,command.PaymentId,LogLevel.Info,"Process_Payment_1","Received Process Payment Request");
            await _busClient.PublishAsync(logCreated);

            return Accepted($"processes/{command.PaymentId}");
        }
    }
}