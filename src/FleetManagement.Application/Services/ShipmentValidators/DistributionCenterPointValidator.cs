using FleetManagement.Application.DTO.response;
using FleetManagement.Application.Enums;
using FleetManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetManagement.Application.Services
{
    public class DistributionCenterPointValidator : IDistributionCenterPointValidator
    {
        private readonly IShipmentService _shipmentService;

        public DistributionCenterPointValidator(
            IShipmentService shipmentService
           )
        {
            _shipmentService = shipmentService;
        }

        public List<string> getValidShipments(List<string> barcodes)
        {
            List<string> validBarcodes = new List<string>();

            foreach (var barcode in barcodes)
            {
                var validBarcode = _shipmentService.GetByBarcodeAndDeliveryPointType(barcode, (int)DeliveryPointType.DistributionCenter);
                if (validBarcode != null)
                {
                    validBarcodes.Add(validBarcode.Barcode);
                }
            }
            return validBarcodes;
        }
    }
}
