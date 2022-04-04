using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Application.DTO
{
    public class ShipmentServiceModel
    {
        public string Barcode { get; set; }
        public int DeliveryPointForUnloading { get; set; }
        public int ShipmentType { get; set; }
        public int Status { get; set; }
    }
}
