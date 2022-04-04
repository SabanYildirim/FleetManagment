using FleetManagement.Application.DTO.response;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Application.Interfaces
{
    public interface IDeliveryPointValidator
    {
        List<string> getValidShipments(List<string> barcodes);
    }
}
