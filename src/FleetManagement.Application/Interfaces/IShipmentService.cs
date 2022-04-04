using FleetManagement.Application.DTO;
using FleetManagement.Application.Enums;
using FleetManagement.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Application.Interfaces
{
    public interface IShipmentService
    {
        int AddShipment(ShipmentServiceModel shipmentModel);
        void UpdateStatus(string barcode, ShipmentStatus status);
        void UpdateStatus(List<string> barcodes, ShipmentStatus status);
        ShipmentServiceModel GetByBarcodeAndDeliveryPointType(string barcode, int deliveryPointType);
        bool HasShipment(string barcode);
        ShipmentServiceModel GetByBarcode(string barcode);
        List<string> GetShipmentBarcodes(List<string> barcodes);
    }
}
