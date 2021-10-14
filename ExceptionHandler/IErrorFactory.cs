using System;
using core.Autofac;
namespace core.ExceptionHandler
{
  public interface IErrorFactory : IScopedDependency
  {
    Exception InvalidToken();
    Exception AccessDenied();
    Exception ThisUserIsAltered();
    Exception FileNotFound();
    Exception InValidVerificationCode();
    Exception XssDetect();
    Exception ApiProtectorAttackDetect();
    Exception PhoneFormatError();
    Exception EmailFormatError();
    Exception ResourceNotFound(string message = null);
    Exception ResourceNotFound(object id, string name = null, string message = null);
  }
}