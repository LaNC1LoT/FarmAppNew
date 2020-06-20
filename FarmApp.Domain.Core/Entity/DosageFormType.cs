using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Entity
{
    public class DosageFormType
    {
        public DosageFormType()
        {
            Drugs = new HashSet<ApiMethodRole>();
        }
        public int Id { get; set; }
        public string DosageForm { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual ICollection<ApiMethodRole> Drugs { get; set; }
    }
}
