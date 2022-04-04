using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FleetManagement.Infrastructure.Entities
{
    [Table("Assignments")]
    public class AssignmentEntity : BaseEntity
    {
        public string PackageBarcode { get; set; }
        public string BagBarcode { get; set; }
    }
}
