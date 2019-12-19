using Gateway.Api.Repositories;
using Gateway.Api.Services;
using Gateway.Common.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HistoryController : Controller
    {
        protected HistoryController()
        {
        }

        private readonly IBusClient _busClient;
        private readonly IPaymentResultService _paymentResultService;

        public HistoryController(IBusClient busClient,
            IPaymentResultService paymentResultService)
        {
            _busClient = busClient;
            _paymentResultService = paymentResultService;
        }
        [HttpGet("hello")]
        public IActionResult GetHello() => Content("Hello from History Controller API!");

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var history = await _paymentResultService.GetAsync(id);
            if (history == null)
            {
                return NotFound();
            }
            Guid merchantid = Guid.NewGuid();
            var merchantId = Guid.Parse(User.Identity.Name);
            if (history.MerchantId != merchantId)
            {
                return Unauthorized();
            }
            var logCreated = new CreateLog(merchantid, Guid.Empty, LogLevel.Info, "Get_Details",
                "Get payment " + id + "details");
            await _busClient.PublishAsync(logCreated);

            return Json(history);
        }

        [HttpGet("")]
        public async Task<IActionResult> BrowseAsync()
        {
            var merchantId = Guid.Parse(User.Identity.Name);

            var results = await _paymentResultService.BrowseAsync(merchantId);

            var logCreated = new CreateLog(merchantId, Guid.Empty, LogLevel.Info, "Get_Details",
                "Browse all payments");
            await _busClient.PublishAsync(logCreated);

            return Json(results); ;
        }
    }
}
