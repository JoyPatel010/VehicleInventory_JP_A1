using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VehicleInventory.Application.DTOs
{
    public class CreateVehicleDto
    {
        [Required]
        public string VehicleCode { get; set; }

        [Required]
        public string LocationId { get; set; }

        [Required]
        public string VehicleType { get; set; }
    }
}
