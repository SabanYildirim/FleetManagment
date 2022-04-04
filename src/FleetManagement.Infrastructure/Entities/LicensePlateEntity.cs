using FleetManagement.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FleetManagement.Infrastructure
{
    [Table("LicensePlate")]
    public class LicensePlateEntity : BaseEntity
    {
        public string Plate { get; set; }
    }
}
