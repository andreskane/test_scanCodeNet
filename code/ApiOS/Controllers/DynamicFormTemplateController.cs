using ApiOS.Controllers.Base;
using Application.Dto.Params.DynamicForm;
using Application.RequestModels.CommandRequestModels;
using Application.RequestModels.QueriesRequestModels;
using Application.ResponseModels.CommandResponseModels;
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

    public class DynamicFormTemplateController : ApiControllerBase
    {
        private readonly ILogger<DynamicFormTemplateController> _logger;

        public DynamicFormTemplateController(ILogger<DynamicFormTemplateController> logger, IMediator mediator)
        {
            Mediator = mediator;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<GetDynamicFormTemplateByIdQueryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        [Route("{id}")]
        public async Task<ActionResult<GenericResponse<GetDynamicFormTemplateByIdQueryResponse>>> GetDynamicFormTemplateById(Int64 id)
        {
            try
            {
                var response = await Mediator.Send(new GetDynamicFormTemplateByIdQueryRequest { Id = id });
                if (response.DynamicFormTemplate == null)
                    return Ok(new GenericResponse<GetDynamicFormTemplateByIdQueryResponse>(response, StatusGenericResponse.NoContent));

                return Ok(new GenericResponse<GetDynamicFormTemplateByIdQueryResponse>(response, StatusGenericResponse.OK));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the DynamicForm.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(GenericResponse<GetDynamicFormTemplateByFiltersQueryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<GetDynamicFormTemplateByFiltersQueryResponse>>> Pagination([FromQuery] FilterDynamicForm filter)
        {
            try
            {
                return new GenericResponse<GetDynamicFormTemplateByFiltersQueryResponse>(await Mediator.Send(new GetDynamicFormTemplateByFiltersQueryRequest { Filter = filter }));
            }
            catch (Exception ex)
            {
                return ApiMenssageError(_logger, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(GenericResponse<CreateDynamicFormTemplateCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<CreateDynamicFormTemplateCommandResponse>>> CreateDynamicFormTemplate([FromBody] CreateDynamicFormTemplateCommandRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest("Invalid data.");

            try
            {
                var response = new GenericResponse<CreateDynamicFormTemplateCommandResponse>(await Mediator.Send(request), StatusGenericResponse.Created);
                return CreatedAtAction(nameof(GetDynamicFormTemplateById), new { id = response.Result.DynamicFormTemplate.Id }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the DynamicForm.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(GenericResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<UpdateDynamicFormTemplateCommandResponse>>> UpdateDynamicFormTemplate([FromBody] UpdateDynamicFormTemplateCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.DynamicFormTemplate.Id == null)
                throw new BadRequestException("Invalid Id");

            var response = await Mediator.Send(new UpdateDynamicFormTemplateCommandRequest { DynamicFormTemplate = request.DynamicFormTemplate });
            if (response == null)
                return new GenericResponse<UpdateDynamicFormTemplateCommandResponse>(StatusGenericResponse.NotFound);

            return new GenericResponse<UpdateDynamicFormTemplateCommandResponse>(response, StatusGenericResponse.OK);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(GenericResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<DeleteDynamicFormTemplateCommandResponse>>> DeleteDynamicFormTemplate([FromQuery] DeleteDynamicFormTemplateCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
                throw new BadRequestException("Invalid Id");

            var response = await Mediator.Send(new DeleteDynamicFormTemplateCommandRequest { Id = request.Id });
            if (response == null)
                return new GenericResponse<DeleteDynamicFormTemplateCommandResponse>(StatusGenericResponse.NotFound);

            return new GenericResponse<DeleteDynamicFormTemplateCommandResponse>(response, StatusGenericResponse.OK);
        }
    }
}
