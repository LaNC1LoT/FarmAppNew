using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
using FarmAppServer.Services;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;

namespace FarmAppServer.Middlewares
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IValidation validation, ICustomLogger logger)
        {
            if (await validation.IsValidationAsync(context, logger))
            {
                await _next.Invoke(context);
            }
            else
            {
                if (logger.Log.StatusCode != null) context.Response.StatusCode = logger.Log.StatusCode.Value;
                context.Response.ContentType = "application/json; charset=utf-8";
                await context.Response.WriteAsync(logger.ResponseBody.ToString(), Encoding.UTF8);
            }
        }
    }
}
