using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Models.Vendors
{
    public class PostVendorDto
    {
        public string VendorName { get; set; }
        public string ProducingCountry { get; set; }
        public bool IsDomestic { get; set; }
    }
}
