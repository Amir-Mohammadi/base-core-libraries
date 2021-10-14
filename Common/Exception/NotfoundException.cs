using System.Net;
using core.ExceptionHandler;
namespace base_core.Common.Exception
{
  public class NotfoundException : AppException
  {
    public NotfoundException() : base(HttpStatusCode.NotFound)
    {
    }
    public NotfoundException(int code, object additionalData = null) : base(code, HttpStatusCode.Forbidden, additionalData)
    {
    }
  }
}