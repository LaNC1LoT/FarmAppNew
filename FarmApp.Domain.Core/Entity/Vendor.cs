using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Entity
{
    public class Vendor
    {
        public Vendor()
        {
            Drugs = new HashSet<Drug>();
        }
        public int Id { get; set; }
        public string VendorName { get; set; }
        public string ProducingCountry { get; set; }
        //public bool? IsDomestic { get; set; }//отечественный
        public bool? IsDeleted { get; set; } = false;
        public virtual ICollection<Drug> Drugs { get; set; }
    }
}
