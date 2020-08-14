using CentralDeErros.Business.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CentralDeErros.Business.Middlewares
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;

        public ErrorHandler(RequestDelegate next)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int status = 500;
            var stackTrace = String.Empty;
            string message = exception.Message;

            var exceptionType = exception.GetType();

            if (exceptionType == typeof(ArgumentException) ||
                exceptionType == typeof(InvalidOperationException))
                status = 400;
            if (exceptionType == typeof(NotFoundException))
                status = 404;
            if (exceptionType == typeof(DuplicatedEntity))
                status = 409;

            var result = JsonSerializer.Serialize(new { 
                error = typeof(DuplicateNameException).Name,
                statusCode = status,
                message
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = status;

            return context.Response.WriteAsync(result);
        }
    }
}
