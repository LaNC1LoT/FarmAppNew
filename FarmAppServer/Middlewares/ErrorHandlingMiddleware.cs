using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Models;
using FarmAppServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FarmAppServer.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILoggerDb loggerDb)
        {
            var originalBody = context.Response.Body;
            var responseBody = new MemoryStream();
            context.Response.Body = responseBody;
            context.Request.EnableBuffering();

            
            int.TryParse(context.User.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value, out var userId);
            int.TryParse(context.User.Claims?.FirstOrDefault(c => c.Type == "RoleId")?.Value, out var roleId);
            Log log = new Log
            {
                UserId = userId,
                RoleId = roleId
            };

            try
            {
                log = await GetLogAsync(context, context.Request.Headers, context.Request.Body, log);
                loggerDb.WriteRequest(log);
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                log.Exception = ex.ToString();
                await HandleExceptionAsync(context, ex);
            }
            finally
            {
                log = await GetLogAsync(context, context.Response.Headers, context.Response.Body, log);
                loggerDb.WriteResponse(log);
                await responseBody.CopyToAsync(originalBody);
                responseBody.Dispose();
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json; charset=utf-8";
            return context.Response.WriteAsync(JsonSerializer.Serialize(new { Error = ex.Message }));
        }

        private async Task<Log> GetLogAsync(HttpContext context, IHeaderDictionary header, Stream body, Log log)
        {
            log.HttpMethod = context?.Request?.Method;
            log.PathUrl = context?.Request?.Path;
            log.StatusCode = context?.Response?.StatusCode;
            log.Header = GetHeader(header);
            log.Body = await GetBodyAsync(body);
            return log;
        }

        private string GetHeader(IHeaderDictionary header)
        {
            var stringBuilder = new StringBuilder();
            header.ToList().ForEach(row =>
            {
                stringBuilder.Append($"{row.Key}: {row.Value} {Environment.NewLine}");
            });
            var result = stringBuilder?.ToString();
            return  string.IsNullOrWhiteSpace(result) ? null : result.Length > 4000 ? result.Substring(0, 4000) : result;
        }

        public async Task<string> GetBodyAsync(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(stream, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            stream.Seek(0, SeekOrigin.Begin);
            return string.IsNullOrWhiteSpace(body) ? null : body.Length > 4000 ? body.Substring(0, 4000) : body;
        }
    }
}
