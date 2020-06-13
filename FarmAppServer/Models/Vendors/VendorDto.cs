using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Models
{
    public class VendorDto
    {
        public int Id { get; set; }
        public string VendorName { get; set; }
        public string ProducingCountry { get; set; }
        public bool IsDomestic { get; set; }
        public bool IsDeleted { get; set; }
    }
}
