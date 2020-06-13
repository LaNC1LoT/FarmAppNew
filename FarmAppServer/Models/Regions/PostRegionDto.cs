using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Models.Regions
{
    public class PostRegionDto
    {
        [Required] public int ParentId { get; set; }//parent id
        [Required] public string regionName { get; set; }
        [Required] public int RegionTypeId { get; set; }
        [Required] public uint Population { get; set; }
    }
}
