using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Entity
{
    public class Stock
    {
        public Stock()
        {
            Drugs = new HashSet<Drug>();
        }
        public int Id { get; set; }
        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public virtual ICollection<Drug> Drugs { get; set; }
    }
}
