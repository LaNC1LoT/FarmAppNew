using System.ComponentModel.DataAnnotations;

namespace FarmAppServer.Models
{
    public class PharmacyDto
    {
        [Required] public int? PharmacyId { get; set; }
        [Required] public string PharmacyName { get; set; }
        [Required] public int RegionId { get; set; }
        [Required] public bool? IsMode { get; set; }
        [Required] public bool? IsType { get; set; }
        [Required] public bool? IsNetwork { get; set; }
    }
}