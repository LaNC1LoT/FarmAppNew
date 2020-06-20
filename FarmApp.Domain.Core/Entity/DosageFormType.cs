using System.Collections.Generic;

namespace FarmApp.Domain.Core.Entity
{
    public class DosageFormType
    {
        public DosageFormType()
        {
            Drugs = new HashSet<Drug>();
        }
        public int Id { get; set; }
        public string DosageForm { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual ICollection<Drug> Drugs { get; set; }
    }
}
