using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FarmAppServer.Services
{
    public class AuthOperationAttribute : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var filterDescriptor = context.ApiDescription.ActionDescriptor.FilterDescriptors;
            var isAuthorized = filterDescriptor.Select(filterInfo => filterInfo.Filter).Any(filter => filter is AuthorizeFilter);
            var allowAnonymous = filterDescriptor.Select(filterInfo => filterInfo.Filter).Any(filter => filter is IAllowAnonymousFilter);

            if (isAuthorized && !allowAnonymous)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<IParameter>();

                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "Authorization",
                    In = "header",
                    Description = "JWT access token",
                    Required = true,
                    Type = "string"
                });

                operation.Responses.Add("401", new Response { Description = "Unauthorized" });
                operation.Responses.Add("403", new Response { Description = "Forbidden" });

                operation.Security = new List<IDictionary<string, IEnumerable<string>>>();

                //Add JWT bearer type
                operation.Security.Add(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });

            }
        }
    }
}