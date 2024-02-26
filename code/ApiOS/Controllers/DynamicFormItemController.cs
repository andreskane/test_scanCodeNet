using ApiOS.Controllers.Base;
using Application.Dto.Params.DynamicFormItem;
using Application.Handlers.QueryHandlers;
using Application.RequestModels.CommandRequestModels;
using Application.RequestModels.CommandRequestModels.DynamicFormItem;
using Application.RequestModels.QueriesRequestModels;
using Application.RequestModels.QueriesRequestModels.DynamicFormItem;
using Application.ResponseModels.CommandResponseModels;
using Application.ResponseModels.CommandResponseModels.DynamicFormItem;
using Application.ResponseModels.QueriesResponseModels;
using Application.ResponseModels.QueriesResponseModels.DynamicFormItem;
using ConnectureOS.Framework.Helpers.Response;
using ConnectureOS.Framework.Net.RestClient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DynamicFormItemController : ApiControllerBase
    {
        private readonly ILogger<DynamicFormItemController> _logger;

        public DynamicFormItemController(ILogger<DynamicFormItemController> logger, IMediator mediator)
        {
            Mediator = mediator;
            _logger = logger;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(GenericResponse<GetDynamicFormItemByIdQueryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]

        public async Task<ActionResult<GenericResponse<GetDynamicFormItemByIdQueryResponse>>> GetWDynamicFormById([FromQuery] Int64 id)
        {
            try
            {
                var response = await Mediator.Send(new GetDynamicFormItemByIdQueryRequest { Id = id });
                if (response.WDynamicForm == null)
                    return Ok(new GenericResponse<GetDynamicFormItemByIdQueryResponse>(response, StatusGenericResponse.NoContent));

                return Ok(new GenericResponse<GetDynamicFormItemByIdQueryResponse>(response, StatusGenericResponse.OK));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the DynamicForm.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(GenericResponse<GetWDynamicFormByDynamicFormIdResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]

        public async Task<ActionResult<GenericResponse<GetWDynamicFormByDynamicFormIdResponse>>> GetDynamicFormItemByDynamicFormId([FromQuery] Int64 idDynamicForm)
        {
            try
            {
                var response = await Mediator.Send(new GetWDynamicFormByDynamicFormIdRequest { Id = idDynamicForm });
                if (response.WDynamicForm == null)
                    return Ok(new GenericResponse<GetWDynamicFormByDynamicFormIdResponse>(response, StatusGenericResponse.NoContent));

                return Ok(new GenericResponse<GetWDynamicFormByDynamicFormIdResponse>(response, StatusGenericResponse.OK));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the DynamicForm.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



        [AllowAnonymous]
        [HttpGet("Last/Published")]
        [ProducesResponseType(typeof(GenericResponse<GetDynamicFormItemResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<GenericResponse<GetDynamicFormItemResponse>>> GetLastPublishedDynamicFormItemDynamicForm([FromQuery] long planId, [FromQuery] string zipCode)
        {
            try
            {
                var response = await Mediator.Send(new GetLastPublishedDynamicFormItemRequest { ProductId = planId, ZipCode = zipCode });
                if (response.WDynamicForm == null)
                    return Ok(new GenericResponse<GetDynamicFormItemResponse>(response, StatusGenericResponse.NoContent));

                return new GenericResponse<GetDynamicFormItemResponse>(response, StatusGenericResponse.OK);
            }
            catch (Exception ex)
            {
                return ApiMenssageError(_logger, ex.Message);
            }
        }

        [HttpGet("Component")]
        [ProducesResponseType(typeof(GenericResponse<GetDynamicFormComponentByFiltersQueryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]

        public async Task<ActionResult<GenericResponse<GetDynamicFormComponentByFiltersQueryResponse>>> GetDynamicFormComponentRule([FromQuery] FilterDynamicFormComponent filter)
        {
            try
            {
                var response = await Mediator.Send(new GetDynamicFormComponentByFiltersQueryRequest { Filter = filter });
                if (response.Component == null)
                    return Ok(new GenericResponse<GetDynamicFormComponentByFiltersQueryResponse>(response, StatusGenericResponse.NoContent));

                return new GenericResponse<GetDynamicFormComponentByFiltersQueryResponse>(response, StatusGenericResponse.OK);
            }
            catch (Exception ex)
            {
                return ApiMenssageError(_logger, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(GenericResponse<CreateDynamicFormItemCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]

        public async Task<ActionResult<GenericResponse<CreateDynamicFormItemCommandResponse>>> CreateDynamicForm([FromBody] CreateDynamicFormItemCommandRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest("Invalid data.");

            if (request.WDynamicForm.DynamicFormTemplateId != null && request.WDynamicForm.DynamicFormId != null)
            {
                return BadRequest("Invalid request. Please send only one between DynamicFormId and DynamicFormTemplateId.");
            }

            try
            {
                var response = new GenericResponse<CreateDynamicFormItemCommandResponse>(await Mediator.Send(new CreateDynamicFormItemCommandRequest { WDynamicForm = request.WDynamicForm }), StatusGenericResponse.Created);
                  return response;
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

        public async Task<ActionResult<GenericResponse<UpdateDynamicFormItemCommandResponse>>>
        UpdateDynamicFormDynamicForm([FromBody] UpdateDynamicFormItemCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.WDynamicForm.Id == null)
                throw new BadRequestException("Invalid Id");

            var response = await Mediator.Send(new UpdateDynamicFormItemCommandRequest { WDynamicForm = request.WDynamicForm });
            if (response == null)
                return new GenericResponse<UpdateDynamicFormItemCommandResponse>(StatusGenericResponse.NotFound);

            return new GenericResponse<UpdateDynamicFormItemCommandResponse>(response, StatusGenericResponse.OK);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(GenericResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(404)]
        [Produces("application/json")]

        public async Task<ActionResult<GenericResponse<DeleteDynamicFormItemCommandResponse>>>
        DeleteDynamicFormDynamicForm([FromQuery] DeleteDynamicFormItemCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
                throw new BadRequestException("Invalid Id");

            var response = await Mediator.Send(new DeleteDynamicFormItemCommandRequest { Id = request.Id });
            if (response == null)
                return new GenericResponse<DeleteDynamicFormItemCommandResponse>(StatusGenericResponse.NotFound);

            return new GenericResponse<DeleteDynamicFormItemCommandResponse>(response, StatusGenericResponse.OK);
        }

        [HttpPatch()]
        [ProducesResponseType(typeof(GenericResponse<PatchDynamicFormItemCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]

        public async Task<ActionResult<GenericResponse<PatchDynamicFormItemCommandResponse>>> PatchTenant([FromBody] PatchDynamicFormItemCommandRequest request)
        {
            if (request?.DynamicFormItemId == null) throw new BadRequestException("Invalid Id");

            return new GenericResponse<PatchDynamicFormItemCommandResponse>(await Mediator.Send(request), StatusGenericResponse.OK);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(GenericResponse<CopyDynamicFormItemCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]

        public async Task<ActionResult<GenericResponse<CopyDynamicFormItemCommandResponse>>> CopyDynamicFormItem([FromBody] CopyDynamicFormItemCommandRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest("Invalid data.");

            try
            {
                var response = new GenericResponse<CopyDynamicFormItemCommandResponse>(await Mediator.Send(request), StatusGenericResponse.Created);
                 return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the DynamicForm.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
