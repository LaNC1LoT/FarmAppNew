using System;

namespace FarmAppServer.Models
{
    public class SaleDto
    {
        public long Id { get; set; }
        public int DrugId { get; set; }
        public string DrugName { get; set; }
        public int PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        public int? SaleImportFileId { get; set; }
        public string FileName { get; set; }
        public DateTime? SaleDate { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public bool? IsDiscount { get; set; }
    }
}
