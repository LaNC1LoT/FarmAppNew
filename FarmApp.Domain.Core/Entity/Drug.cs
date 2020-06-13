using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Entity
{
    public class Drug
    {
        public Drug()
        {
            Sales = new HashSet<Sale>();
        }
        public int Id { get; set; }
        public string DrugName { get; set; }
        public int CodeAthTypeId { get; set; }
        public int VendorId { get; set; }
        public bool? IsGeneric { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public virtual CodeAthType CodeAthType { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
