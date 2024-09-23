using Microsoft.AspNetCore.Http;
using ProjectManagementSystem.API.Exceptions;
using System.Net;


namespace ProjectManagementSystem.API.Middlewares
{
    public class GlobalErrorHandlerMiddleware
    {
        RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;

        public GlobalErrorHandlerMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


                var respnse = new ApiResponse((int)HttpStatusCode.InternalServerError, ex.Message);



                await httpContext.Response.WriteAsJsonAsync(respnse);

            }

        }
    }
}
