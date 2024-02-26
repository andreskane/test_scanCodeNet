using ApiOS.Controllers.Base;
using Application.Dto.Params;
using Application.RequestModels.QueriesRequestModels.Components;
using Application.ResponseModels.QueriesResponseModels.Components;
using ConnectureOS.Framework.Helpers.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ComponentsController : ApiControllerBase
    {
        private readonly ILogger<ComponentsController> _logger;

        public ComponentsController(ILogger<ComponentsController> logger, IMediator mediator)
        {
            Mediator = mediator;
            _logger = logger;
        }
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(GenericResponse<GetTemplateComponentsQueryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]

        public async Task<ActionResult<GenericResponse<GetTemplateComponentsQueryResponse>>> GetTemplateComponentsByFilters([FromQuery] FilterTemplateComponentsDto filter)
        {
            try
            {
                return new GenericResponse<GetTemplateComponentsQueryResponse>(await Mediator.Send(new GetTemplateComponentsQueryRequest { Filter = filter }));
            }
            catch (Exception ex)
            {
                return ApiMenssageError(_logger, ex.Message);
            }
        }


    }
}
