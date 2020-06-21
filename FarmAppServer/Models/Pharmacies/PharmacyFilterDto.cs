namespace FarmAppServer.Models.Pharmacies
{
    public class PharmacyFilterDto
    {
        public int Id { get; set; }
        public int? PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public bool IsMode { get; set; }
        public bool IsType { get; set; }
        public bool IsNetwork { get; set; }
        public bool IsDeleted { get; set; }
    }
}
