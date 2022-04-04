using FleetManagement.Application.DTO.response;
using FleetManagement.Common;
using FleetManagement.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FleetManagement.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="next"></param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke Exceptional Handling
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (FleetManagementBaseException ex) 
            when (ex is DataNotFoundException ||  
            ex is ValidationException || 
            ex is DuplicateDataException)
            {
                await HandleError(context,ex.ErrorCode , ex.Message,HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                await HandleError(context, AppErrorCodeConstants.UnknownErrorCode, ex.Message,HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleError(HttpContext context, int errorCode, string errorMessage,HttpStatusCode httpStatusCode)
        {
            var responseModel = new ExceptionResponse(errorCode, errorMessage);  
            
            var responseBody = JsonSerializer.Serialize(responseModel, new JsonSerializerOptions
            {
                IgnoreNullValues = true
            });

            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;
            await context.Response.WriteAsync(responseBody, Encoding.UTF8);
        }
    }
}
