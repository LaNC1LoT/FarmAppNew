using FarmAppServer.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FarmAppServer.Services
{
    public interface IValidation
    {
        Task<bool> IsValidationAsync(HttpContext context, ICustomLogger logger);
    }
}