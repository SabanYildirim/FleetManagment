using FleetManagement.Application.DTO.response;
using FleetManagement.Application.Enums;
using FleetManagement.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FleetManagement.Application.Services
{
    public class TransferCenterDeliveryPointValidator : ITransferCenterDeliveryPointValidator
    {
        private readonly IShipmentService _shipmentService;
        private readonly IAssignmentService _assignmentService;

        public TransferCenterDeliveryPointValidator(
            IShipmentService shipmentService,
            IAssignmentService assignmentService
           )
        {
            _shipmentService = shipmentService;
            _assignmentService = assignmentService;
        }

        public List<string> getValidShipments(List<string> barcodes)
        {
            List<string> validBarcodes = new List<string>();

            foreach (var barcode in barcodes)
            {                
                var validBarcode = _shipmentService.GetByBarcodeAndDeliveryPointType(barcode, (int)DeliveryPointType.TransferCenter);
                if (validBarcode != null && _assignmentService.HasAssigment(validBarcode.Barcode))
                {
                    validBarcodes.Add(validBarcode.Barcode);
                }
            }

            return validBarcodes;
        }
    }
}
