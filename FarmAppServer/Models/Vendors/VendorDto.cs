namespace FarmAppServer.Models
{
    public class VendorDto
    {
        public int Id { get; set; }
        public string VendorName { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public bool? IsDomestic { get; set; }
    }
}
