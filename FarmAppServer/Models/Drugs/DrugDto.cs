using System.ComponentModel.DataAnnotations;

namespace FarmAppServer.Models.Drugs
{
    public class DrugDto
    {
        public int Id { get; set; }
        public string DrugName { get; set; }
        public int CodeAthTypeId { get; set; }
        public string CodeAthTypeName { get; set; }
        public string Code { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public bool IsDomestic { get; set; }
        public bool IsGeneric { get; set; }
        public bool IsDeleted { get; set; }
    }
}
