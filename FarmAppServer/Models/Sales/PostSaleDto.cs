using System;
using System.ComponentModel.DataAnnotations;

namespace FarmAppServer.Models.Sales
{
    public class PostSaleDto
    {
        [Required] public int DrugId { get; set; }
        public int? SaleImportFileId { get; set; }
        [Required] public int PharmacyId { get; set; }
        [Required] public DateTime SaleDate { get; set; }
        [Required] public decimal Price { get; set; }
        [Required] public int Quantity { get; set; }
        [Required] public bool IsDiscount { get; set; }
    }
}
