using ConnectureOS.Framework.Net.RestClient;
using FluentValidation.Results;

namespace Application.Helper.Exceptions;

public class FakeExceptionFactory
{
    public static Exception Create(int code)
    {
        switch (code)
        {
            case 400:  //99999400
                return new BadRequestException("[BadRequestException] something very bad hapened");
            case 404: //99999404
                return new NotFoundException("[NotFoundException] something very bad hapened");
            case 460: //99999460
                return new CorruptedFileException("[CorruptedFileException] something very bad hapened");
            case 480: //99999480
                return new ValidationException(new List<ValidationFailure> {
            new ValidationFailure("badProperty","failed"),
            new ValidationFailure("veryBadProperty","failedUgly")
          });
            case 501: //99999501
                return new FakeException(501, "Not Implemented Status501NotImplemented");
            case 502: //99999502
                return new FakeException(502, "Bad Gateway Status502BadGateway");
            case 503: //99999503
                return new FakeException(503, "Service Unavailable Status503ServiceUnavailable");
            case 504: //99999504
                return new FakeException(504, "Gateway Timeout Status504GatewayTimeout");
            case 505: //99999505
                return new FakeException(505, "HTTP Version Not Supported Status505HttpVersionNotsupported");
            case 506: //99999506
                return new FakeException(506, "Variant Also Negotiates Status506VariantAlsoNegotiates");
            case 507: //99999507
                return new FakeException(507, "Insufficient Storage Status507InsufficientStorage");
            case 508: //99999508
                return new FakeException(508, "Loop Detected Status508LoopDetected");
            case 510: //99999509
                return new FakeException(510, "Not Extended Status510NotExtended");
            case 511: //99999510
                return new FakeException(511, "Network Authentication Required Status511NetworkAuthenticationRequired");
            case 560: //99999560
                return new FakeException(560, "Internal Server Error Status500InternalServerError");
            case 599: //99999599
                return new FakeException(599, "SQL Errors");
            default:
                return new FakeException(500, "Network Connect Timeout Error");
        }

    }
}
