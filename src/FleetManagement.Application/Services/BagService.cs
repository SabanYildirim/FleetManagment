using FleetManagement.Application.DTO;
using FleetManagement.Application.Interfaces;
using FleetManagement.Application.Enums;
using FleetManagement.Common;
using Microsoft.Extensions.Logging;
using FleetManagement.Common.Exceptions;
using System;

namespace FleetManagement.Application.Services
{
    public class BagService : IBagService
    {
        private readonly IDeliveryPointService _deliveryPointService;
        private readonly IShipmentService _shipmentService;
        private readonly ILogger<BagService> _logger;

        public BagService(
            IDeliveryPointService deliveryPointService,
            IShipmentService shipmentService,
            ILogger<BagService> logger
            )
        {
            _deliveryPointService = deliveryPointService;
            _shipmentService = shipmentService;
            _logger = logger;
        }

        public int AddBag(BagServiceModel model)
        {
            try
            {
                ShipmentServiceModel serviceModel = new ShipmentServiceModel();
                serviceModel.Barcode = model.Barcode;
                serviceModel.DeliveryPointForUnloading = model.DeliveryPointforUnloading;
                serviceModel.Status = (int)ShipmentStatus.Created;
                serviceModel.ShipmentType = (int)ShipmentType.Bag;

                return _shipmentService.AddShipment(serviceModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
