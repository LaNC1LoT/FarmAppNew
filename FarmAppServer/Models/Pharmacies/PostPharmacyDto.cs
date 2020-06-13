using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Models.Pharmacies
{
    public class PostPharmacyDto
    {
        public int? PharmacyId { get; set; }
        [Required] public string PharmacyName { get; set; }
        [Required] public int RegionId { get; set; }
        /// <summary>
        /// Время работы
        /// </summary>
        [Required] public bool IsMode { get; set; }
        /// <summary>
        /// Тип частаная/государственная
        /// </summary>
        [Required] public bool IsType { get; set; }
        /// <summary>
        /// Сеть или нет
        /// </summary>
        [Required] public bool IsNetwork { get; set; }
    }
}
