using ApiOS.Controllers.Base;
using Application.Dto.Params.BulkProcess;
using Application.Handlers.QueryHandlers.BulkProcess;
using Application.RequestModels.CommandRequestModels.BulkProcess;
using Application.ResponseModels.CommandResponseModels.BulkProcess;
using ConnectureOS.Framework.Helpers.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BulkProcessController : ApiControllerBase
    {

        private readonly ILogger _logger;

        public BulkProcessController(ILoggerFactory loggerFactory, IMediator mediator)
        {
            Mediator = mediator;
            _logger = loggerFactory.CreateLogger(GetType());

        }


        [HttpGet("Pagination")]
        [ProducesResponseType(typeof(GenericResponse<GetAllBulkProcessQueryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<GetAllBulkProcessQueryResponse>>> GetAllBulckProcess([FromQuery] FilterBulkProcess filter)
        {
            try
            {
                return new GenericResponse<GetAllBulkProcessQueryResponse>(await Mediator.Send(new GetAllBulkProcessQueryRequest { Filter = filter }));
            }
            catch (Exception ex)
            {
                return ApiMenssageError(_logger, ex.Message);
            }
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(GenericResponse<CreateBulkProcessCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<CreateBulkProcessCommandResponse>>> CreateBulkProcess([FromBody] CreateBulkProcessCommandRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest("Invalid data.");

            try
            {


                var response = new GenericResponse<CreateBulkProcessCommandResponse>(await Mediator.Send(request), StatusGenericResponse.Created);
                return response;


            }
            catch (Exception ex)
            {
                return ApiMenssageError(_logger, ex.Message);
            }
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(GenericResponse<UpdateBulkProcessCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<UpdateBulkProcessCommandResponse>>> UpdateBulkProcess([FromBody] UpdateBulkProcessCommandRequest request)
        {
            if (request == null)
                return BadRequest("Invalid data.");

            try
            {
                var response = new GenericResponse<UpdateBulkProcessCommandResponse>(await Mediator.Send(request), StatusGenericResponse.OK);
                return response;
            }
            catch (Exception ex)
            {
                return ApiMenssageError(_logger, ex.Message);
            }
        }


        [HttpPost("Update")]
        [ProducesResponseType(typeof(GenericResponse<UpdateBulkProcessCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<UpdateBulkProcessCommandResponse>>> PostUpdateBulkProcess([FromBody] UpdateBulkProcessCommandRequest request)
        {
            if (request == null)
                return BadRequest("Invalid data.");

            try
            {
                var response = new GenericResponse<UpdateBulkProcessCommandResponse>(await Mediator.Send(request), StatusGenericResponse.OK);
                return response;
            }
            catch (Exception ex)
            {
                return ApiMenssageError(_logger, ex.Message);
            }
        }


        [HttpPost("AsignDynamicForm")]
        [ProducesResponseType(typeof(GenericResponse<AsignDynamicFormBulkProcessCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<GenericResponse<AsignDynamicFormBulkProcessCommandResponse>>> AsignDynamicFormBulkProcess([FromBody] AsignDynamicFormBulkProcessCommandRequest request)
        {
            if (request == null)
                return BadRequest("Invalid data.");

            try
            {
                var response = new GenericResponse<AsignDynamicFormBulkProcessCommandResponse>(await Mediator.Send(request), StatusGenericResponse.OK);

                return response;
            }
            catch (Exception ex)
            {
                return ApiMenssageError(_logger, ex.Message);
            }
        }

        [HttpGet("GetComponetsForBulkByBulk")]
        [ProducesResponseType(typeof(GenericResponse<GetComponetsForBulkByBulkQueryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<GenericResponse<GetComponetsForBulkByBulkQueryResponse>>> GetComponetsForBulkByBulk([FromQuery] GetComponetsForBulkByBulkQueryRequest request)
        {
            try
            {
                var response = await Mediator.Send(request);
                if (response.Components == null)
                    return Ok(new GenericResponse<GetComponetsForBulkByBulkQueryResponse>(response, StatusGenericResponse.NoContent));

                return new GenericResponse<GetComponetsForBulkByBulkQueryResponse>(response, StatusGenericResponse.OK);
            }
            catch (Exception ex)
            {
                return ApiMenssageError(_logger, ex.Message);
            }
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(GenericResponse<GetAllComponentsQueryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<GenericResponse<GetAllComponentsQueryResponse>>> GetComponetsListByBulkId([FromQuery] GetAllComponentQueryRequest request)
        {
            try
            {
                var response = await Mediator.Send(request);
                if (response.ComponentList == null)
                    return Ok(new GenericResponse<GetAllComponentsQueryResponse>(response, StatusGenericResponse.NoContent));

                return new GenericResponse<GetAllComponentsQueryResponse>(response, StatusGenericResponse.OK);
            }
            catch (Exception ex)
            {
                return ApiMenssageError(_logger, ex.Message);
            }
        }

        


    }


}
