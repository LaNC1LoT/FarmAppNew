using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Entity
{
    public class RegionType
    {
        public RegionType()
        {
            Regions = new HashSet<Region>();
        }

        public int Id { get; set; }
        public string RegionTypeName { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual ICollection<Region> Regions { get; set; }
    }
}
