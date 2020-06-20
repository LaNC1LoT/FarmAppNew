using System;

namespace FarmApp.Domain.Core.Entity
{
    public class SaleImportFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Message { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
