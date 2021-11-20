using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using TestApp.BusinessLogic.Exceptions;

namespace TestApp.Middlewares
{
    public class ExceptionMiddleware
    {
        RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException() is IStatusCode statusCode)
                    httpContext.Response.StatusCode = statusCode.StatusCode;

                if (httpContext.Response.StatusCode == 200)
                    httpContext.Response.StatusCode = 400;

                httpContext.Response.ContentType = "text/html";

                await httpContext.Response.WriteAsync(ex.GetBaseException().Message);
            }
        }
    }
}
