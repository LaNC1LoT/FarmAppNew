using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.StoredProcedure
{
    public class ChartStock
    {
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string ParentPharmacy { get; set; }
        public string PharmacyName { get; set; }
        public string DrugName { get; set; }
        public bool? IsGeneric { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? Quantity { get; set; }
    }
}
