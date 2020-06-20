using System.ComponentModel.DataAnnotations;

namespace FarmAppServer.Models.Regions
{
    public class PostRegionTypeDto
    {
        [Required] public string RegionTypeName { get; set; }
    }
}
