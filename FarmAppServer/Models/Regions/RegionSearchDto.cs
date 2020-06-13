using System.Collections.Generic;
using FarmApp.Domain.Core.Entity;

namespace FarmAppServer.Models.Regions
{
    public class RegionSearchDto
    {
        public int Id { get; set; }

        public Region ChildRegion { get; set; }
        //public int? RegionId { get; set; }
        //public int RegionTypeId { get; set; }
        public string RegionName { get; set; }
        public int Population { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public virtual Region ParentRegion { get; set; }
        public virtual RegionType RegionType { get; set; }
        //public virtual ICollection<Region> Regions { get; set; }
    }
}