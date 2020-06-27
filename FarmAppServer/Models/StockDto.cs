using System;

namespace FarmAppServer.Models
{
    public class StockDto
    {
        public int Id { get; set; }
        public int PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        public int DrugId { get; set; }
        public string DrugName { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Quantity { get; set; }
    }
}
