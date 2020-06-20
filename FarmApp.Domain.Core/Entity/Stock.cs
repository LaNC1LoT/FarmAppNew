using System;
using System.Collections.Generic;

namespace FarmApp.Domain.Core.Entity
{
    public class Stock
    {
        public Stock()
        {
            Pharmacies = new HashSet<Pharmacy>();
            Drugs = new HashSet<Drug>();
        }
        public int Id { get; set; }
        public int PharmacyId { get; set; }
        public int DrugId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Quantity { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual ICollection<Pharmacy> Pharmacies { get; set; }
        public virtual ICollection<Drug> Drugs { get; set; }
    }
}
