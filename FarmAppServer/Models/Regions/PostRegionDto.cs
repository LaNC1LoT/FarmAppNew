using System.ComponentModel.DataAnnotations;

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
