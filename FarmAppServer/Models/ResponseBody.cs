using Newtonsoft.Json;
using System;

namespace FarmAppServer.Models
{
    public class ResponseBody
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime ResponseTime { get; set; } = DateTime.Now;
        public string Header { get; set; } 
        public string Result { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
