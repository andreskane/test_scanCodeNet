using ApiOS.Controllers.Base;
using Application.Dto.Params;
using Application.RequestModels.CommandRequestModels.Rule;
using Application.RequestModels.QueriesRequestModels.Rule;
using Application.ResponseModels.CommandResponseModels.Rules;
using Application.ResponseModels.QueriesResponseModels.Rule;
using ConnectureOS.Framework.Helpers.Response;
using ConnectureOS.Framework.Net.RestClient;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RulesController : ApiControllerBase
    {
        private readonly ILogger<RulesController> _logger;

        public RulesController(ILogger<RulesController> logger, IMediator mediator)
        {
            Mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<GetRuleByIdQueryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        [Route("{id}")]
        public async Task<ActionResult<GenericResponse<GetRuleByIdQueryResponse>>> GetRuleById(Int64 id)
        {
            try
            {
                var response = await Mediator.Send(new GetRuleByIdQueryRequest { Id = id });
                if (response.Rule == null)
                    return Ok(new GenericResponse<GetRuleByIdQueryResponse>(response, StatusGenericResponse.NoContent));

                return Ok(new GenericResponse<GetRuleByIdQueryResponse>(response, StatusGenericResponse.OK));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Workflow.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(GenericResponse<CreateRuleCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<CreateRuleCommandResponse>>> CreateRule([FromBody] CreateRuleCommandRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest("Invalid data.");

            try
            {
                var response = new GenericResponse<CreateRuleCommandResponse>(await Mediator.Send(request), StatusGenericResponse.Created);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the Workflow.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(GenericResponse<GetRulesByFilterQueryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<GetRulesByFilterQueryResponse>>> Pagination([FromQuery] RulesFilter filter)
        {
            try
            {
                return new GenericResponse<GetRulesByFilterQueryResponse>(await Mediator.Send(new GetRulesByFilterQueryRequest { Filter = filter }));
            }
            catch (Exception ex)
            {
                return ApiMenssageError(_logger, ex.Message);
            }
        }


        [HttpPut]
        [ProducesResponseType(typeof(GenericResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<UpdateRuleCommandResponse>>>
        UpdateRule([FromBody] UpdateRuleCommandRequest request)
        {
            if (request == null) throw new BadRequestException("Invalid data.");
            if (request.Rule.Id == null)
                throw new BadRequestException("Invalid Id");

            var response = await Mediator.Send(request);
            if (response == null)
                return new GenericResponse<UpdateRuleCommandResponse>(StatusGenericResponse.NotFound);

            return new GenericResponse<UpdateRuleCommandResponse>(response, StatusGenericResponse.OK);
        }

        [HttpPut("[action]")]
        [ProducesResponseType(typeof(GenericResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<UpdateEnabledRuleCommandResponse>>>
        UpdateEnabledRule([FromBody] UpdateEnabledRuleCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.RuleId == null)
                throw new BadRequestException("Invalid Id");

            var response = await Mediator.Send(request);
            if (response == null)
                return new GenericResponse<UpdateEnabledRuleCommandResponse>(StatusGenericResponse.NotFound);

            return new GenericResponse<UpdateEnabledRuleCommandResponse>(response, StatusGenericResponse.OK);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(GenericResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<DeleteRuleCommandResponse>>>
            DeleteWorkflow([FromQuery] DeleteRuleCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
                throw new BadRequestException("Invalid Id");

            var response = await Mediator.Send(new DeleteRuleCommandRequest { Id = request.Id });
            if (response == null)
                return new GenericResponse<DeleteRuleCommandResponse>(StatusGenericResponse.NotFound);

            return new GenericResponse<DeleteRuleCommandResponse>(response, StatusGenericResponse.OK);
        }
    }
}
