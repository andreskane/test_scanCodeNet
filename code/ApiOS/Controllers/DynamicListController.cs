using ApiOS.Controllers.Base;
using Application.Dto.Params.ListValue;
using Application.Handlers.CommandHandlers.ListValue;
using Application.Handlers.QueryHandlers.ListValue;
using Application.RequestModels.CommandRequestModels.ListDefinition;
using Application.RequestModels.CommandRequestModels.ListValue;
using Application.RequestModels.QueriesRequestModels.ListValue;
using Application.ResponseModels.CommandResponseModels.ListDefinition;
using Application.ResponseModels.CommandResponseModels.ListValue;
using Application.ResponseModels.QueriesResponseModels.ListValue;
using ConnectureOS.Framework.Helpers.Response;
using ConnectureOS.Framework.Net.RestClient;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiOS.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DynamicListController : ApiControllerBase
    {

        private readonly ILogger<DynamicListController> _logger;

        public DynamicListController(ILogger<DynamicListController> logger, IMediator mediator)
        {
            Mediator = mediator;
            _logger = logger;
        }
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(GenericResponse<GetListRowQueryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<GetListRowQueryResponse>>> GetListValuesByFilter([FromQuery] ListValueFilter filter)
        {
            try
            {
                return new GenericResponse<GetListRowQueryResponse>(await Mediator.Send(new GetListRowQueryRequest { Filter = filter }));
            }
            catch (Exception ex)
            {
                return ApiMenssageError(_logger, ex.Message);
            }
        }
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(GenericResponse<GetAllDynamicListResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<GetAllDynamicListResponse>>> GetListByFilter([FromQuery] ListValueFilter filter)
        {
            try
            {
                return new GenericResponse<GetAllDynamicListResponse>(await Mediator.Send(new GetAllDynamicListRequest { Filter = filter }));
            }
            catch (Exception ex)
            {
                return ApiMenssageError(_logger, ex.Message);
            }
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(GenericResponse<CreateListDefinitionCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<CreateListDefinitionCommandResponse>>> CreateListDefinition([FromBody] CreateListDefinitionCommandRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest("Invalid data.");

            try
            {
                var response = await Mediator.Send(request);
                return CreatedAtAction(nameof(CreateListDefinition), new { id = response.ListDefinition.Id }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the List Definition.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(GenericResponse<CreateListValueCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<CreateListValueCommandResponse>>> ListValueBulkInsert([FromBody] CreateListValueCommandRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest("Invalid data.");

            try
            {
                var response = await Mediator.Send(request);
                return CreatedAtAction(nameof(ListValueBulkInsert), null, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the List Definition.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("[action]")]
        [ProducesResponseType(typeof(GenericResponse<UpdateListValueCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<CreateListValueCommandResponse>>> ListValueBulkEdit([FromBody] UpdateListValueCommandRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest("Invalid data.");

            try
            {
                var response = await Mediator.Send(request);
                return CreatedAtAction(nameof(ListValueBulkEdit), null, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the List Definition.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(GenericResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<DeleteListValueCommandResponse>>>
            DeleteDynamicList(Int64 id)
        {
            if (id == null)
                throw new BadRequestException("Invalid Id");

            var response = await Mediator.Send(new DeleteListValueCommandRequest { ListValueId = id });
            if (response == null)
                return new GenericResponse<DeleteListValueCommandResponse>(StatusGenericResponse.NotFound);

            return new GenericResponse<DeleteListValueCommandResponse>(response, StatusGenericResponse.OK);
        }
    }
}
