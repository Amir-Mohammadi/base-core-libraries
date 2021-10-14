using System.Net;
using core.ExceptionHandler;
namespace base_core.Common.Exception
{
  public class UnauthorizedException : AppException
  {
    public UnauthorizedException() : base(HttpStatusCode.NotFound)
    {
    }
    public UnauthorizedException(int code, object additionalData = null) : base(code, HttpStatusCode.Forbidden, additionalData)
    {
    }
  }
}