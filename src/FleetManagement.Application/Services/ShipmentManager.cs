using FleetManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FleetManagement.Application.Enums;
using Microsoft.Extensions.Logging;

namespace FleetManagement.Application.Services
{
    public class ShipmentManager : IShipmentManager
    {
        private readonly IShipmentService _shipmentService;
        private readonly IAssignmentService _assignmentService;
        private readonly ILogger<ShipmentManager> _logger;

        public ShipmentManager(
            IShipmentService shipmentService,
            IAssignmentService assignmentService,
            ILogger<ShipmentManager> logger)
        {
            _shipmentService = shipmentService;
            _assignmentService = assignmentService;
            _logger = logger;
        }

        public void ProcessShipment(string barcode)
        {
            try
            {
                _shipmentService.UpdateStatus(barcode, ShipmentStatus.Unloaded);
                var linkedBarcodes = getLinkedBarcodes(barcode);
                foreach (var linkedBarcode in linkedBarcodes)
                {
                    _shipmentService.UpdateStatus(linkedBarcode, ShipmentStatus.Unloaded);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                throw;
            }
        }

        private List<string> getLinkedBarcodes(string barcode)
        {
            try
            {
                List<string> linkedBarcodes = new List<string>();

                var shipment = _shipmentService.GetByBarcode(barcode);
                if (shipment.ShipmentType == (int)ShipmentType.Bag)
                {
                    var packToBagBarcodes = _assignmentService.GetByBagBarcodes(barcode)?.Select(s => s.PackageBarcode).ToList();
                    if (packToBagBarcodes != null)
                        linkedBarcodes.AddRange(packToBagBarcodes);
                }
                else
                {
                    var bagToPackagesBarcodes = _assignmentService.GetByPackageBarcodes(barcode)?.Select(s => s.BagBarcode).ToList();
                    if (bagToPackagesBarcodes != null)
                        linkedBarcodes.AddRange(bagToPackagesBarcodes);
                }
                return linkedBarcodes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
