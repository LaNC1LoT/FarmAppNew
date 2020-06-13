namespace FarmAppServer.Models.Regions
{
    public class RegionDto
    {
        public int Id { get; set; }
        public int RegionId { get; set; }
        public string ParentRegionName { get; set; }
        public string RegionName { get; set; }
        public int RegionTypeId { get; set; }
        public string RegionTypeName { get; set; }
        public int Population { get; set; }
        public bool IsDeleted { get; set; }
    }
}
