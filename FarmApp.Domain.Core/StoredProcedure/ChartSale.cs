using System;

namespace FarmApp.Domain.Core.StoredProcedure
{
    public class ChartSale
    {
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string ParentPharmacy { get; set; }
        public string PharmacyName { get; set; }
        public string DrugName { get; set; }
        public string Code { get; set; }
        public bool? IsGeneric { get; set; }
        public DateTime? SaleDate { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public bool? IsDiscount { get; set; }
    }
}
