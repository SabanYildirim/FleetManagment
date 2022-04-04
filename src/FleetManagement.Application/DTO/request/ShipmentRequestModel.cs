using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Application.DTO.request
{
    public class ShipmentRequestModel
    {
        public string plate { get; set; }
        public List<Route> route { get; set; }
    }

    public class Route
    {
        public int deliveryPoint { get; set; }
        public List<Delivery> deliveries { get; set; }
    }

    public class Delivery
    {
        public string barcode { get; set; }
    }
}
