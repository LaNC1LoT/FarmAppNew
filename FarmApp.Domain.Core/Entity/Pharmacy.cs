using System.Collections.Generic;

namespace FarmApp.Domain.Core.Entity
{
    public class Pharmacy
    {
        public Pharmacy()
        {
            Pharmacies = new HashSet<Pharmacy>();
            Sales = new HashSet<Sale>();
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public int? PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        public int RegionId { get; set; }
        /// <summary>
        /// Время работы
        /// </summary>
        public bool? IsMode { get; set; }

        /// <summary>
        /// Тип частаная/государственная
        /// </summary>
        public bool? IsType { get; set; }

        /// <summary>
        /// Сеть или нет
        /// </summary>
        public bool? IsNetwork { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual Pharmacy ParentPharmacy { get; set; }
        public virtual Region Region { get; set; }
        public virtual ICollection<Pharmacy> Pharmacies { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
