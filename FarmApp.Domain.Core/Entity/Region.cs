﻿using System.Collections.Generic;

namespace FarmApp.Domain.Core.Entity
{
    public class Region
    {
        public Region()
        {
            Regions = new HashSet<Region>();
            Pharmacies = new HashSet<Pharmacy>();
            Vendors = new HashSet<Vendor>();
        }
        public int Id { get; set; }
        public int? RegionId { get; set; }//parent region id
        public int RegionTypeId { get; set; }
        public string RegionName { get; set; }
        public int Population { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual Region ParentRegion { get; set; }
        public virtual RegionType RegionType { get; set; }
        public virtual ICollection<Region> Regions { get; set; }
        public virtual ICollection<Pharmacy> Pharmacies { get; set; }
        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}
