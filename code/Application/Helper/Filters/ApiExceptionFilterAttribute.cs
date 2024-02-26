using Application.Helper.Exceptions;
using ConnectureOS.Framework.Helpers.Response;
using ConnectureOS.Framework.Message;
using ConnectureOS.Framework.Net.RestClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ValidationException = ConnectureOS.Framework.Net.RestClient.ValidationException;

namespace Application.Helper.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{

    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
    private readonly ILogger<ApiExceptionFilterAttribute> _logger;

    public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
    {
        _logger = logger;
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(BadRequestException), HandleBadRequestException },
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(CorruptedFileException), HandleCorruptedFileException },
            { typeof(ConnectureOS.Framework.Net.RestClient.ValidationException), HandleValidationException},
            { typeof(FakeException), HandleFakeException},
        };
    }

    public override void OnException(ExceptionContext context)
    {

        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        HandleUnknownException(context);
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        var menssageError = new GenericMessage();
        menssageError.AddMessageError();
        _logger.LogError($"Code: {menssageError.GetCode()} {context.Exception.Message}");

        context.Result = new ObjectResult(menssageError)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var exception = context.Exception as ValidationException;
        var details = new GenericResponse(StatusGenericResponse.BadRequest, exception.Errors);

        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

    private void HandleCorruptedFileException(ExceptionContext context) =>
        HandleException(context, StatusGenericResponse.BadRequest, x => new ObjectResult(x) { StatusCode = 460 });

    private void HandleBadRequestException(ExceptionContext context)
    {
        var exception = context.Exception as BadRequestException;
        var details = new GenericResponse(StatusGenericResponse.BadRequest);
        details.Messages.Add(context.Exception.Message);
        details.StatusResponse.Code = exception.Code ?? 400;
        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(ExceptionContext context) =>
        HandleException(context, StatusGenericResponse.NotFound, x => new NotFoundObjectResult(x));

    private void HandleException(ExceptionContext context, StatusGenericResponse status, Func<object, IActionResult> actionResult)
    {
        var details = new GenericResponse(status);
        details.Messages.Add(context.Exception.Message);

        context.Result = actionResult.Invoke(details);
        context.ExceptionHandled = true;
    }
    private void HandleFakeException(ExceptionContext context)
    {
        var exception = context.Exception as FakeException;
        var details = new GenericResponse();
        details.Messages.Add(context.Exception.Message);
        details.StatusResponse.Code = exception.Code ?? 400;
        details.StatusResponse.Message = exception.Message;

        context.Result = new ObjectResult(details)
        {
            StatusCode = details.StatusResponse.Code,
        };
        context.ExceptionHandled = true;
    }

}