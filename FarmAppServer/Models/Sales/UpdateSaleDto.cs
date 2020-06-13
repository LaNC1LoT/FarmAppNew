using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Models.Sales
{
    public class UpdateSaleDto
    {
        [Required] public DateTime SaleDate { get; set; }//15.09.1911
        [Required] public decimal Price { get; set; }//41199
        [Required] public int Quantity { get; set; }//44
        [Required] public bool IsDiscount { get; set; }//true
    }
}
