using FarmApp.Domain.Core.Entity;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.Extantions;
using FarmAppServer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAppServer.Services
{
    public class Validation : IValidation
    {
        private readonly FarmAppContext Ctx;

        public Validation(FarmAppContext appContext)
        {
            Ctx = appContext;
        }

        public async Task<bool> IsValidationAsync(HttpContext context, ICustomLogger logger)
        {
            logger.Log.UserId = context.User.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
            logger.Log.RoleId = context.User.Claims?.FirstOrDefault(c => c.Type == "RoleId")?.Value;

            logger.Log.HttpMethod = context.Request.Method;
            logger.Log.PathUrl = context.Request.Path;

            var stringBuilder = new StringBuilder();
            context.Request.Headers.ToList().ForEach(row =>
            {
                stringBuilder.Append($"{row.Key}: {row.Value} {Environment.NewLine}");
            });

            logger.Log.HeaderRequest = stringBuilder.ToString();

            string body;
            context.Request.EnableBuffering();
            using (var reader = new StreamReader(context.Request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true))
            {
                body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            if (!body.TryParseJson(out RequestBody request, out string errorMsg))
            {
                logger.Log.FactTime = DateTime.Now;
                logger.Log.Param = body?.Length > 4000 ? body.Substring(0, 4000) : body;

                logger.Log.StatusCode = 400;
                logger.ResponseBody = new ResponseBody { Header = "Ошибка", Result = $"Неверные параметры запроса JSON: {errorMsg}" };
                return false;
            }
            else
            {
                logger.Log.FactTime = DateTime.Now;
                logger.Log.MethodRoute = request?.MethodRoute;
                logger.Log.Param = request?.Param?.Length > 4000 ? request.Param.Substring(0, 4000) : request?.Param;
                logger.Log.RequestTime = request?.RequestTime ?? DateTime.Now;

                ApiMethod apiMethod;
                if (request != null)
                    apiMethod = Ctx.ApiMethods.FirstOrDefault(x => x.ApiMethodName == request.MethodRoute && x.IsDeleted == false);
                else
                    apiMethod = Ctx.ApiMethods.FirstOrDefault(x => x.PathUrl == logger.Log.PathUrl && x.HttpMethod == logger.Log.HttpMethod && x.IsDeleted == false);

                if (apiMethod == null)
                {
                    logger.Log.StatusCode = 400;
                    logger.ResponseBody = new ResponseBody { Header = "Ошибка", Result = $"Метод '{logger.Log.MethodRoute}' не найден!" };
                    return false;
                }

                if (apiMethod.IsNeedAuthentication ?? true)
                {
                    if (!string.IsNullOrWhiteSpace(logger.Log.UserId) && !string.IsNullOrWhiteSpace(logger.Log.RoleId) &&
                        int.TryParse(logger.Log.UserId, out int resultUser) && int.TryParse(logger.Log.RoleId, out int resultRole))
                    {
                        var user = Ctx.Users.FirstOrDefault(x => x.Id == resultUser && x.RoleId == resultRole);
                        if (user == null)
                        {
                            logger.Log.StatusCode = 401;
                            logger.ResponseBody = new ResponseBody { Header = "Ошибка", Result = $"Неверный токен!" };
                            return false;
                        }

                        if (user.IsDeleted ?? true)
                        {
                            logger.Log.StatusCode = 401;
                            logger.ResponseBody = new ResponseBody { Header = "Ошибка", Result = $"Пользователь заблокирован!" };
                            return false;
                        }
                        
                        var  apiMethodRoles = Ctx.ApiMethodRoles.FirstOrDefault(x => x.ApiMethodId == apiMethod.Id && x.RoleId == user.RoleId);
                        if (apiMethodRoles == null)
                        {
                            logger.Log.StatusCode = 403;
                            logger.ResponseBody = new ResponseBody { Header = "Ошибка", Result = $"Недостаточно прав!" };
                            return false;
                        }

                        if (apiMethodRoles.IsDeleted ?? true)
                        {
                            logger.Log.StatusCode = 400;
                            logger.ResponseBody = new ResponseBody { Header = "Ошибка", Result = $"Метод '{logger.Log.MethodRoute}' недоступен для данной роли!" };
                            return false;
                        }
                    }
                    else
                    {
                        logger.Log.StatusCode = 401;
                        logger.ResponseBody = new ResponseBody { Header = "Ошибка", Result = $"Неверный токен!" };
                        return false;
                    }
                }

                if (apiMethod.IsNotNullParam == true && string.IsNullOrWhiteSpace(logger.Log.Param))
                {
                    logger.Log.StatusCode = 400;
                    logger.ResponseBody = new ResponseBody { Header = "Ошибка", Result = $"Метод '{logger.Log.MethodRoute}' не принимает пустые параметры!" };
                    return false;
                }

                if (apiMethod.HttpMethod.ToLower() != logger.Log.HttpMethod.ToLower() || apiMethod.PathUrl.ToLower() != logger.Log.PathUrl.ToLower())
                {
                    logger.Log.StatusCode = 400;
                    logger.ResponseBody = new ResponseBody { Header = "Ошибка", Result = $"Метод '{logger.Log.MethodRoute}' вызывается при помощи '{apiMethod.HttpMethod}' запроса по адресу {apiMethod.PathUrl}!" };
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(errorMsg))
                logger.Log.Exception += $"JSON parse request: {errorMsg} {Environment.NewLine}";

            return true;
        }
    }
}
