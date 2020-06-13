using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FarmApp.Domain.Core.Entity;

namespace FarmAppServer.Models.Sales
{
    public class SaleDto
    {
        [Required] public long Id { get; set; }
        [Required] public int DrugId { get; set; }
        [Required] public string DrugName { get; set; }
        [Required] public int PharmacyId { get; set; }
        [Required] public string PharmacyName { get; set; }
        [Required] public string SaleImportFileName { get; set; }
        [Required] public DateTime SaleDate { get; set; }
        [Required] public decimal Price { get; set; }
        [Required] public int Quantity { get; set; }
        [Required] public decimal Amount { get; set; }
        [Required] public bool IsDiscount { get; set; }
        [Required] public bool IsDeleted { get; set; }
    }
}
