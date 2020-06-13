using FarmApp.Infrastructure.Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmAppServer.Extantions;
using FarmAppServer.Models;

namespace FarmAppServer.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICustomLogger _logger;
        public RequestResponseLoggingMiddleware(RequestDelegate next, ICustomLogger logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context);
            await _next.Invoke(context);
            //await LogResponse(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            var request = context.Request;
            _logger.Log.UserId = context.User.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
            _logger.Log.RoleId = context.User.Claims?.FirstOrDefault(c => c.Type == "RoleId")?.Value;

            _logger.Log.HttpMethod = context.Request.Method;
            _logger.Log.PathUrl = context.Request.Path;

            //log header request
            var stringBuilder = new StringBuilder();
            context.Request.Headers.ToList().ForEach(row =>
            {
                stringBuilder.Append($"{row.Key}: {row.Value} {Environment.NewLine}");
            });

            _logger.Log.HeaderRequest = stringBuilder.ToString();

            //log body
            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            _logger.Log.FactTime = DateTime.Now;
            _logger.Log.MethodRoute = request?.PathBase;
            _logger.Log.Param = body?.Length > 4000 ? body.Substring(0, 4000) : body;
            _logger.Log.RequestTime = DateTime.Now;
        }

        //private static string ReadStreamInChunks(Stream stream)
        //{
        //    const int readChunkBufferLength = 4096;
        //    stream.Seek(0, SeekOrigin.Begin);
        //    using var textWriter = new StringWriter();
        //    using var reader = new StreamReader(stream);
        //    var readChunk = new char[readChunkBufferLength];
        //    int readChunkLength;
        //    do
        //    {
        //        readChunkLength = reader.ReadBlock(readChunk,
        //                                           0,
        //                                           readChunkBufferLength);
        //        textWriter.Write(readChunk, 0, readChunkLength);
        //    } while (readChunkLength > 0);
        //    return textWriter.ToString();
        //}


        private async Task LogResponse(HttpContext context)
        {
            var response = context.Response;

            response.Body.Seek(0, SeekOrigin.Begin);
            var textResponse = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            if (textResponse.TryParseJson(out ResponseBody responseBody, out string errorMsg))
            {
                _logger.Log.ResponseId = responseBody?.Id;
                _logger.Log.ResponseTime = responseBody?.ResponseTime;
                _logger.Log.Header = responseBody?.Header;
                _logger.Log.Result = responseBody?.Result?.Length > 4000 ? responseBody.Result.Substring(0, 4000) : responseBody?.Result;
            }
            else
            {
                _logger.Log.ResponseTime = DateTime.Now;
                _logger.Log.Result = textResponse?.Length > 400 ? textResponse.Substring(0, 4000) : textResponse;
            }

            _logger.Log.StatusCode = response.StatusCode;
           
            if (!string.IsNullOrWhiteSpace(errorMsg))
                _logger.Log.Exception += $"JSON parse response: {errorMsg} {Environment.NewLine}";


            var stringBuilder = new StringBuilder();
            response.Headers.ToList().ForEach(row =>
            {
                stringBuilder.Append($"{row.Key}: {row.Value} {Environment.NewLine}");
            });

            _logger.Log.HeaderResponse = stringBuilder.ToString();
            
            //?????await _next(context);
        }
    }
}
