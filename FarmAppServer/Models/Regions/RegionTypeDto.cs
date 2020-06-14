using System.ComponentModel.DataAnnotations;

namespace FarmAppServer.Models
{
    public class RegionTypeDto
    {
        public int Id { get; set; }
        public string RegionTypeName { get; set; }
        public bool IsDeleted { get; set; }
    }
}