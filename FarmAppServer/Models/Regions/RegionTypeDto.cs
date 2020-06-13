using System.ComponentModel.DataAnnotations;

namespace FarmAppServer.Models
{
    public class RegionTypeDto
    {
        [Required] public int Id { get; set; }
        [Required] public string RegionTypeName { get; set; }
        [Required] public bool IsDeleted { get; set; }
    }
}