using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Application.DTO.response
{
    public class ShipmentResponseModel
    {
        public string plate { get; set; }
        public List<RouteResponseModel> route { get; set; } = new List<RouteResponseModel>();
    }

    public class RouteResponseModel
    {
        public int deliveryPoint { get; set; }
        public List<DeliveryResponseModel> deliveries { get; set; } = new List<DeliveryResponseModel>();
    }

    public class DeliveryResponseModel
    {
        public string barcode { get; set; }
        public int State { get; set; }
    }
}
