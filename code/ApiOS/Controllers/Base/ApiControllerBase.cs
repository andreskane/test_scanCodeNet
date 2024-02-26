
using ConnectureOS.Framework.Message;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using System.Dynamic;
namespace ApiOS.Controllers.Base;

[Authorize]
public class ApiControllerBase : ControllerBase
{

    protected IMediator Mediator;
    [ApiExplorerSettings(IgnoreApi = true)]
    public bool IsPropertyExist(dynamic settings, string name)
    {
        if (settings is ExpandoObject)
            return ((IDictionary<string, object>)settings).ContainsKey(name);

        return settings.GetType().GetProperty(name) != null;
    }

    /// <summary>
    /// APIs the menssage error.
    /// </summary>
    /// <param name="message">The message.</param>
    [ApiExplorerSettings(IgnoreApi = true)]
    public ObjectResult ApiMenssageError(ILogger _logger, string message)
    {
        var otherMessage = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        var menssageError = new GenericMessage();

        menssageError.AddMessageError();
        _logger.LogError($"Code: {menssageError.GetCode()} {message} - {otherMessage}");
        return StatusCode(StatusCodes.Status500InternalServerError, menssageError);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public ObjectResult ApiMenssageBadRequest(ILogger _logger, string message)
    {
        var menssageError = new GenericMessage();
        menssageError.AddMessage(message);
        _logger.LogError(message);
        return StatusCode(StatusCodes.Status400BadRequest, menssageError);
    }
}
