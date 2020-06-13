using System;
using System.Linq;
using Newtonsoft.Json;

namespace FarmAppServer.Services.Paging
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IQueryable<T> Data { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}