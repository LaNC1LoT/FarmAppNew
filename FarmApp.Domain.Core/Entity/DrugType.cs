using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Domain.Core.Entity
{
    public class DrugType
    {
        public DrugType()
        {
            Drugs = new HashSet<Drug>();
        }
        public int Id { get; set; }
        public string DrugName { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual ICollection<Drug> Drugs { get; set; }
    }
}
