using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Application.DTO
{
    public class PackageServiceModel
    {
        public string Barcode { get; set; }
        public int DeliveryPointforUnloading { get; set; }
        public int VolumetricWeight { get; set; }
    }
}
