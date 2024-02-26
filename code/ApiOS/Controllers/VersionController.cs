using ApiOS.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiOS.Controllers;
[ApiController]
[Route("api/[controller]")]

public class VersionController : ApiControllerBase
{
    private readonly ILogger<VersionController> _logger;

    public VersionController(ILogger<VersionController> logger, IMediator mediator)
    {
        Mediator = mediator;
        _logger = logger;

    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult> Get()
    {
        var strVersion = new ApiOS.Api.Common().GetAssemblyVersion();
        return Ok(strVersion);
    }

}
