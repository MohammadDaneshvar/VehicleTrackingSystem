using Framework.Application;
using Framework.Domain.Enum;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NLog;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace App.Service.Distributor.Common
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private static ILogger _logger = LogManager.GetCurrentClassLogger();
        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            StatusEnum statusEnum = StatusEnum.UnkownError;
            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    statusEnum = StatusEnum.InvalidModelState;
                    result = JsonConvert.SerializeObject(validationException.ValidationResult.ErrorMessage);
                    break;
                case Framework.Application.Common.Exceptions.ValidationException validationException:
                    statusEnum = StatusEnum.InvalidModelState;
                    result = JsonConvert.SerializeObject(validationException.Failures);
                    break;
                case Framework.Application.Common.Exceptions.NotFoundException _:
                    result = JsonConvert.SerializeObject(exception.Message);
                    statusEnum = StatusEnum.NotFound;
                    break;
                case ExceptionResult e:
                    result = JsonConvert.SerializeObject(e.Message);
                    statusEnum = e.StatusEnum;
                    break;
                case UnauthorizedAccessException e:
                    result = JsonConvert.SerializeObject(e.Message);
                    statusEnum = StatusEnum.Unauthorized;
                    break;
            }

            var code = HttpStatusCode.InternalServerError;
            switch (statusEnum)
            {
                
                
                case StatusEnum.Unauthorized:
                    code = HttpStatusCode.Unauthorized;
                    break;
                case StatusEnum.Forbidden:
                    code = HttpStatusCode.Forbidden;
                    break;
                case StatusEnum.NotFound:
                    code = HttpStatusCode.NotFound;
                    break;
                //case StatusEnum.ItIsNotPossibleToCreateANewAccount:
                //    break;
                //case StatusEnum.CanNotCharge:
                //    break;
                //case StatusEnum.OrganizationLimitRequest:
                //    break;
                //case StatusEnum.InvalidAccount:
                //    break;
                //case StatusEnum.StockNotEnough:
                //    break;
                //case StatusEnum.InvalidActionPrice:
                //    break;
                //case StatusEnum.UseSqlServer:
                //case StatusEnum.UnkownError:
                //    code = HttpStatusCode.InternalServerError;
                //    break;
                default:
                    code = HttpStatusCode.BadRequest;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (string.IsNullOrEmpty(result))
            {
                result = JsonConvert.SerializeObject("internal Error");
            }

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = result, code = statusEnum }));
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
