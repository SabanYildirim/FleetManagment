using System.ComponentModel.DataAnnotations.Schema;

namespace FleetManagement.Infrastructure.Entities
{
    [Table("Shipment")]
    public class ShipmentEntity : BaseEntity
    {
        public string Barcode { get; set; }
        public int DeliveryPointForUnloading { get; set; }
        public int ShipmentType { get; set; }
        public int Status { get; set; }
    }
}
