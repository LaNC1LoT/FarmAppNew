using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Models.Drugs
{
    public class PostDrugDto
    {
        [Required]
        public string DrugName { get; set; }

        [Required]
        public int CodeAthTypeId { get; set; }
        
        [Required]
        public int VendorId { get; set; }

        [Required]
        public bool IsDomestic { get; set; }

        [Required]
        public bool IsGeneric { get; set; }
    }
}
