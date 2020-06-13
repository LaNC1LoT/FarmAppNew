using FarmApp.Domain.Core.Entity;

namespace FarmAppServer.Models
{
    public interface ICustomLogger
    {
        Log Log { get; set; }
        ResponseBody ResponseBody { get; set; }
    }
}
