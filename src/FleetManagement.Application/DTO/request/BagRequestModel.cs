using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Application.DTO
{
    public class BagRequestModel
    {
        public string Barcode { get; set; }
        public int DeliveryPointforUnloading { get; set; }
    }
}
