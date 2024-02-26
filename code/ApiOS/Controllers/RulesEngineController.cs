
using ApiOS.Controllers.Base;
using Application.Services.Rules;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RulesEngineController : ApiControllerBase
    {
        private readonly ILogger _logger;
        public RulesEngineController(ILoggerFactory loggerFactory,
            IMediator mediator

            )
        {
            Mediator = mediator;
            _logger = loggerFactory.CreateLogger(GetType());

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("validate")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<IActionResult> ValidateRule([FromBody] ValidateRuleRequest request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("execute")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<IActionResult> ExecuteRule([FromBody] ExecuteRuleRequest request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }

    }


}
