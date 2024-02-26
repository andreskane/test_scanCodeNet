
using ApiOS.Controllers.Base;
using drx.cos.workflow.rulemanager.Services.Rules;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;



namespace drx.cos.workflow.rulemanager.Controllers
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
