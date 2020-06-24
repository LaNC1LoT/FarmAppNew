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
            try
            {
                await GetRequest(context, loggerDb, new Log());
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, loggerDb, new Log());
            }
            finally
            {
                await FinallyWriteBody(context, originalBody, responseBody, loggerDb, new Log());
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex, ILoggerDb loggerDb, Log log)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json; charset=utf-8";
            log.Body = JsonSerializer.Serialize(new { Error = ex.Message });
            log.Exception += ex.ToString();
            loggerDb.WriteException(log);

            return context.Response.WriteAsync(log.Body);
        }

        private async Task FinallyWriteBody(HttpContext context, Stream originalBody, Stream responseBody, ILoggerDb loggerDb, Log log)
        {
            try
            {
                await GetResponse(context.Response, loggerDb, log);
                await responseBody.CopyToAsync(originalBody);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, loggerDb, log);
            }
            finally
            {
                responseBody.Dispose();
            }
        }

        private async Task GetResponse(HttpResponse response, ILoggerDb loggerDb, Log log)
        {
            log.StatusCode = response.StatusCode;
            log.Header = GetHeader(response.Headers);
            log.Body = await GetBody(response.Body);
            loggerDb.WriteResponse(log);
        }

        private async Task GetRequest(HttpContext context, ILoggerDb loggerDb, Log log)
        {
            if (int.TryParse(context.User.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value, out var userId))
                log.UserId = userId;
            if (int.TryParse(context.User.Claims?.FirstOrDefault(c => c.Type == "RoleId")?.Value, out var roleId))
                log.RoleId = roleId;

            log.HttpMethod = context.Request.Method;
            log.PathUrl = context.Request.Path;

            log.Header = GetHeader(context.Request.Headers);
            log.Body = await GetBody(context.Request.Body);

            loggerDb.WriteRequest(log);
        }

        public async Task<string> GetBody(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(stream, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            stream.Seek(0, SeekOrigin.Begin);
            return body?.Length > 4000 ? body.Substring(0, 4000) : body;
        }

        private string GetHeader(IHeaderDictionary header)
        {
            var stringBuilder = new StringBuilder();
            header.ToList().ForEach(row =>
            {
                stringBuilder.Append($"{row.Key}: {row.Value} {Environment.NewLine}");
            });

            return stringBuilder?.Length > 4000 ? stringBuilder?.ToString().Substring(0, 4000) : stringBuilder?.ToString();
        }
    }
}
