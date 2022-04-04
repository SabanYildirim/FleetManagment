using FleetManagement.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using FleetManagement.Application.Enums;
using FleetManagement.Application.DTO.response;

namespace FleetManagement.Application.Services
{
    public class BranchDeliveryPointValidator : IBranchDeliveryPointValidator
    {
        private readonly IShipmentService _shipmentService;
        public BranchDeliveryPointValidator(
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
                var validBarcode = _shipmentService.GetByBarcodeAndDeliveryPointType(barcode, (int)DeliveryPointType.Branch);
                if (validBarcode != null && validBarcode.ShipmentType == (int)ShipmentType.Package)
                {
                    validBarcodes.Add(validBarcode.Barcode);
                }             
            }
            return validBarcodes;
        }
    }
}
