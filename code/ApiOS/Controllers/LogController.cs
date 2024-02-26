using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiOS.Controllers;
[ApiController]
[Route("api/[controller]")]
public class LogController : Base.ApiControllerBase
{
    public LogController(IMediator mediator)
    {
        Mediator = mediator;
    }

    [ProducesResponseType(typeof(Guid?), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult> Get()
    {
        try
        {
            string url = string.Format("logs//log{0}.log", DateTime.Now.ToString("yyyyMMdd"));
            string texto = "";


            using (var fileStream = new FileStream(url, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fileStream, leaveOpen: true))
                {
                    texto = await sr.ReadToEndAsync();
                }
            }




            return Ok(texto); 
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
