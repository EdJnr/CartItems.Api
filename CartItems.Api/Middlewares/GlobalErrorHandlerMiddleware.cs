using CartItems.Api.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CartItems.Api.Middlewares
{
    public class GlobalErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;

        public GlobalErrorHandlerMiddleware(ILogger<GlobalErrorHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await ExceptionHandler(context, ex);
            }
        }


       // method to handle the exceptions
        private async Task ExceptionHandler(HttpContext context, Exception ex)
        {
            _logger.LogError($"Server error at {ex.Source} : {ex.Message} \n Details : {ex.Message}");

            var responseBody = new ApiResponse<string>
            (
                false,
                null,
                (int)HttpStatusCode.InternalServerError,
                ex.Message.ToString()
            );

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var body = JsonSerializer.Serialize(responseBody);
            await context.Response.WriteAsync(body);
        }
    }
}