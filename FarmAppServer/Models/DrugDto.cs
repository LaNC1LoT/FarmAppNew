﻿namespace FarmAppServer.Models
{
    public class DrugDto
    {
        public int Id { get; set; }
        public string DrugName { get; set; }
        public int CodeAthTypeId { get; set; }
        public string Code { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public int DosageFormTypeId { get; set; }
        public string DosageForm { get; set; }
        public bool? IsDomestic { get; set; }
        public bool? IsGeneric { get; set; }
    }
}
