using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using base_core.Common;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace core.ExceptionHandler
{
  public class ExceptionHandlerService : IExceptionHandlerService
  {
    private readonly IHostEnvironment hostEnvironment;
    public ExceptionHandlerService(IHostEnvironment hostEnvironment)
    {
      this.hostEnvironment = hostEnvironment;
    }
    private void importAppExceptionPayload(IExceptionPayload payload, AppException exception)
    {
      payload.Code = exception.Code;
      payload.Title = ErrorCodes.Resolve(exception.Code);
      payload.Info = exception.Info;
    }
    private object createExceptionPayloadForDevelopmentMode(Exception exception)
    {
      var payload = new
      {
        Message = exception.Message,
        StackTrace = exception.StackTrace
      };
      return payload;
    }
    public async Task Handle(HttpContext httpContext, Exception exception)
    {
      var contractResolver = new DefaultContractResolver
      {
        NamingStrategy = new SnakeCaseNamingStrategy()
      };
      var jsonSettings = new JsonSerializerSettings
      {
        ContractResolver = contractResolver,
        Formatting = Formatting.Indented
      };
      string payload = null;
      AppException appException = exception as AppException;
      if (hostEnvironment.IsDevelopment())
      {
        var exceptionToSerialize = new DevelopmentExceptionPayload();
        exceptionToSerialize.Exceptions.Add(createExceptionPayloadForDevelopmentMode(exception));
        if (exception.InnerException != null)
        {
          exceptionToSerialize.Exceptions.Add(createExceptionPayloadForDevelopmentMode(exception.InnerException));
        }
        if (appException != null)
        {
          importAppExceptionPayload(exceptionToSerialize, appException);
        }
        else
        {
          exceptionToSerialize.Code = ErrorCodes.INTERNAL_SERVER_ERROR;
          exceptionToSerialize.Title = ErrorCodes.Resolve(exceptionToSerialize.Code);
          exceptionToSerialize.Info = exception.InnerException == null ? exception.Message : exception.InnerException.Message;
        }
        payload = JsonConvert.SerializeObject(exceptionToSerialize, jsonSettings);
      }
      else
      {
        var exceptionToSerialize = new ExceptionPayload();
        if (appException != null)
        {
          importAppExceptionPayload(exceptionToSerialize, appException);
        }
        else
        {
          exceptionToSerialize.Code = ErrorCodes.INTERNAL_SERVER_ERROR;
          exceptionToSerialize.Title = ErrorCodes.Resolve(ErrorCodes.INTERNAL_SERVER_ERROR);
          exceptionToSerialize.Info = "An internal server error has occurred.";
        }
        payload = JsonConvert.SerializeObject(exceptionToSerialize, jsonSettings);
      }
      httpContext.Response.StatusCode = appException != null ? (int)appException.HttpStatusCode : (int)HttpStatusCode.InternalServerError;
      httpContext.Response.ContentType = "application/json";
      await httpContext.Response.WriteAsync(payload);
    }
  }
}