using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FleetManagement.Infrastructure.Entities
{
    [Table("PackageInfo")]
    public class PackageInfo : BaseEntity
    {
        public int ShipmentId { get; set; }
        public int Weight { get; set; }
    }
}
