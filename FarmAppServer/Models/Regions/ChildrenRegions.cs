namespace FarmAppServer.Models
{
    public class ChildrenRegions
    {
        public int Id { get; set; }//id
        public int? RegionId { get; set; }//parent id
        public string ParentRegionName { get; set; }//parent region name
        public int RegionTypeId { get; set; }
        public string RegionName { get; set; }//region name
        public string RegionTypeName { get; set; }
        public int Population { get; set; }
        public bool? IsDeleted { get; set; }
    }
}