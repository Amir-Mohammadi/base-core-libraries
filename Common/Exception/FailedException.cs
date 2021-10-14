using System.Net;
using core.ExceptionHandler;
namespace base_core.Common.Exception
{
  public class FailedException : AppException
  {
    public FailedException() : base(HttpStatusCode.BadRequest)
    {
    }
    public FailedException(int code, object additionalData = null) : base(code, HttpStatusCode.BadRequest, additionalData)
    {
    }
  }
}