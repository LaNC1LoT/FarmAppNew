using System.Collections.Generic;

namespace FarmApp.Domain.Core.Entity
{
    public class Drug
    {
        public Drug()
        {
            Sales = new HashSet<Sale>();
            Stocks = new HashSet<Stock>();
        }
        public int Id { get; set; }
        public string DrugName { get; set; }
        //public int? DrugId { get; set; }
        public int CodeAthTypeId { get; set; }
        public int VendorId { get; set; }
        public int DosageFormTypeId { get; set; }
        public bool? IsGeneric { get; set; }
        public bool? IsDeleted { get; set; }
        //public virtual DrugType DrugType { get; set; }
        public virtual CodeAthType CodeAthType { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual DosageFormType DosageFormType { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
