using ApiOS.Controllers.Base;
using Application.RequestModels.CommandRequestModels;
using Application.RequestModels.QueriesRequestModels;
using Application.ResponseModels.CommandResponseModel;
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
    public class DynamicFormProductAttributesController : ApiControllerBase
    {
        private readonly ILogger<DynamicFormProductAttributesController> _logger;

        public DynamicFormProductAttributesController(ILogger<DynamicFormProductAttributesController> logger, IMediator mediator)
        {
            Mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<GetDynamicFormProductAttributesByIdQueryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        [Route("{id}")]
        public async Task<ActionResult<GenericResponse<GetDynamicFormProductAttributesByIdQueryResponse>>> GetWProductAttributeById(Int64 id)
        {
            try
            {
                var response = await Mediator.Send(new GetDynamicFormProductAttributesByIdQueryRequest { Id = id });
                if (response.WorkflowProductAttribute == null)
                    return Ok(new GenericResponse<GetDynamicFormProductAttributesByIdQueryResponse>(response, StatusGenericResponse.NoContent));

                return Ok(new GenericResponse<GetDynamicFormProductAttributesByIdQueryResponse>(response, StatusGenericResponse.OK));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Workflow.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

   

        [HttpPost]
        [ProducesResponseType(typeof(GenericResponse<CreateDynamicFormProductAttributesCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<CreateDynamicFormProductAttributesCommandResponse>>> CreateWorkflow([FromBody] CreateDynamicFormProductAttributesCommandRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest("Invalid data.");

            try
            {
                var response = new GenericResponse<CreateDynamicFormProductAttributesCommandResponse>(await Mediator.Send(new CreateDynamicFormProductAttributesCommandRequest { WProductAttribute = request.WProductAttribute }), StatusGenericResponse.Created);
                return CreatedAtAction(nameof(GetWProductAttributeById), new { id = response.Result.WProductAttribute.Id }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the Workflow.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(GenericResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<UpdateDynamicFormProductAttributesCommandResponse>>>
        UpdateDynamicFormProductAttribute([FromBody] UpdateDynamicFormProductAttributesCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.DynamicFormProductAttribute.Id == null)
                throw new BadRequestException("Invalid Id");

            var response = await Mediator.Send(new UpdateDynamicFormProductAttributesCommandRequest { DynamicFormProductAttribute = request.DynamicFormProductAttribute });
            if (response == null)
                return new GenericResponse<UpdateDynamicFormProductAttributesCommandResponse>(StatusGenericResponse.NotFound);

            return new GenericResponse<UpdateDynamicFormProductAttributesCommandResponse>(response, StatusGenericResponse.OK);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(GenericResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<DeleteDynamicFormProductAttributesCommandResponse>>>
        DeleteWorkflow([FromQuery] DeleteDynamicFormProductAttributesCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
                throw new BadRequestException("Invalid Id");

            var response = await Mediator.Send(new DeleteDynamicFormProductAttributesCommandRequest { Id = request.Id });
            if (response == null)
                return new GenericResponse<DeleteDynamicFormProductAttributesCommandResponse>(StatusGenericResponse.NotFound);

            return new GenericResponse<DeleteDynamicFormProductAttributesCommandResponse>(response, StatusGenericResponse.OK);
        }
    }
}
