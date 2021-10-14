using System.Net;
using core.ExceptionHandler;
namespace base_core.Common.Exception
{
  public class ForbiddenException : AppException
  {
    public ForbiddenException() : base(HttpStatusCode.Forbidden)
    {
    }
    public ForbiddenException(int code, object additionalData = null) : base(code, HttpStatusCode.Forbidden, additionalData)
    {
    }
  }
}