using ApiOS.Controllers.Base;
using Application.Dto.Params.DynamicForm;
using Application.RequestModels.CommandRequestModels;
using Application.RequestModels.CommandRequestModels.DynamicFormPlan;
using Application.RequestModels.QueriesRequestModels;
using Application.ResponseModels.CommandResponseModels;
using Application.ResponseModels.CommandResponseModels.DynamicFormPlan;
using Application.ResponseModels.QueriesResponseModels;
using ConnectureOS.Framework.Helpers.Response;
using ConnectureOS.Framework.Net.RestClient;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class DynamicFormController : ApiControllerBase
    {
        private readonly ILogger<DynamicFormController> _logger;

        public DynamicFormController(ILogger<DynamicFormController> logger, IMediator mediator)
        {
            Mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<GetDynamicFormByIdQueryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        [Route("{id}")]
        public async Task<ActionResult<GenericResponse<GetDynamicFormByIdQueryResponse>>> GetDynamicFormById(Int64 id)
        {
            try
            {
                var response = await Mediator.Send(new GetDynamicFormByIdQueryRequest { Id = id });
                if (response.Workflow == null) 
                    return Ok(new GenericResponse<GetDynamicFormByIdQueryResponse>(response, StatusGenericResponse.NoContent));

                return Ok(new GenericResponse<GetDynamicFormByIdQueryResponse>(response, StatusGenericResponse.OK));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the DynamicForm.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(GenericResponse<GetDynamicFormByFiltersQueryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<GetDynamicFormByFiltersQueryResponse>>> Pagination([FromQuery] FilterDynamicForm filter)
        {
            try
            {
                return new GenericResponse<GetDynamicFormByFiltersQueryResponse>(await Mediator.Send(new GetDynamicFormByFiltersQueryRequest { Filter = filter }));
            }
            catch (Exception ex)
            {
                return ApiMenssageError(_logger, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(GenericResponse<CreateDynamicFormCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<CreateDynamicFormCommandResponse>>> CreateDynamicForm([FromBody] CreateDynamicFormCommandRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest("Invalid data.");

            try
            {
                var response = new GenericResponse<CreateDynamicFormCommandResponse>(await Mediator.Send(request), StatusGenericResponse.Created);
                return CreatedAtAction(nameof(CreateDynamicForm), new { id = response.Result.dynamicForm.Id }, response);//todo: change to response.Result.DynamicForm
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the Workflow.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("Plan")]
        [ProducesResponseType(typeof(GenericResponse<CreateDynamicFormPlanCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<CreateDynamicFormPlanCommandResponse>>> CreateWorkflowPlan([FromBody] CreateDynamicFormPlanCommandRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest("Invalid data.");

            try
            {
                var response = new GenericResponse<CreateDynamicFormPlanCommandResponse>(await Mediator.Send(request), StatusGenericResponse.Created);

                if (response.Result.DynamicFormPlan == null)
                    return BadRequest($"Plan {request.PlanId} already published for DynamicForm {request.DynamicFormId}");

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the DynamicForm Plan.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(GenericResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<UpdateDynamicFormCommandResponse>>>
        UpdateWorkflow([FromBody] UpdateDynamicFormCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.Workflow.Id == null)
                throw new BadRequestException("Invalid Id");

            var response = await Mediator.Send(new UpdateDynamicFormCommandRequest { Workflow = request.Workflow });
            if (response == null)
                return new GenericResponse<UpdateDynamicFormCommandResponse>(StatusGenericResponse.NotFound);

            return new GenericResponse<UpdateDynamicFormCommandResponse>(response, StatusGenericResponse.OK);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(GenericResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<DeleteDynamicFormCommandResponse>>>
        DeleteWorkflow([FromQuery] DeleteDynamicFormCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
                throw new BadRequestException("Invalid Id");

            var response = await Mediator.Send(new DeleteDynamicFormCommandRequest { Id = request.Id });
            if (response == null)
                return new GenericResponse<DeleteDynamicFormCommandResponse>(StatusGenericResponse.NotFound);

            return new GenericResponse<DeleteDynamicFormCommandResponse>(response, StatusGenericResponse.OK);
        }
    }
}
