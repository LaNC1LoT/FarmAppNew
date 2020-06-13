using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Models.Regions
{
    public class PostRegionTypeDto
    {
        [Required] public string RegionTypeName { get; set; }
    }
}
