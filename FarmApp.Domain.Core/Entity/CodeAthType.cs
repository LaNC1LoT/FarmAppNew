﻿using System.Collections.Generic;

namespace FarmApp.Domain.Core.Entity
{
    public class CodeAthType
    {
        public CodeAthType()
        {
            CodeAthTypes = new HashSet<CodeAthType>();
            Drugs = new HashSet<Drug>();
        }
        public int Id { get; set; }
        public int? CodeAthId { get; set; }
        public string Code { get; set; }
        public string NameAth { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual CodeAthType CodeAth { get; set; }
        public virtual ICollection<CodeAthType> CodeAthTypes { get; set; }
        public virtual ICollection<Drug> Drugs { get; set; }
    }
}
