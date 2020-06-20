using System;
using System.Collections.Generic;

namespace FarmApp.Domain.Core.Entity
{
    public class Stock
    {
        public int Id { get; set; }
        public int PharmacyId { get; set; }
        public int DrugId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Quantity { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual Pharmacy Pharmacy { get; set; }
        public virtual Drug Drug { get; set; }
    }
}
