using System.ComponentModel.DataAnnotations;

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
