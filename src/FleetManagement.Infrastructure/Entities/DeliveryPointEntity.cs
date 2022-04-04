using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FleetManagement.Infrastructure.Entities
{
    [Table("DeliveryPoint")]
    public class DeliveryPointEntity : BaseEntity
    {
        public string DeliveryPoint { get; set; }
        public int Type { get; set; }
    }
}
