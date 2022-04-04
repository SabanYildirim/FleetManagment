using FleetManagement.Application.DTO;
using FleetManagement.Application.Enums;
using FleetManagement.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace FleetManagement.Application.Services
{
    public class PackageService : IPackagesService
    {
        private readonly IDeliveryPointService _deliveryPointService;
        private readonly IShipmentService _shipmentService;
        private readonly IPackageInfoService _packageInfoService;
        private readonly ILogger<PackageService> _logger;

        public PackageService(
            IDeliveryPointService deliveryPointService,
            IShipmentService shipmentService,
            IPackageInfoService packageInfoService,
            ILogger<PackageService> logger)
        {
            _deliveryPointService = deliveryPointService;
            _shipmentService = shipmentService;
            _packageInfoService = packageInfoService;
            _logger = logger;
        }

        public int AddPackage(PackageServiceModel model)
        {
            try
            {
                ShipmentServiceModel serviceModel = new ShipmentServiceModel();

                serviceModel.Barcode = model.Barcode;
                serviceModel.DeliveryPointForUnloading = model.DeliveryPointforUnloading;
                serviceModel.Status = (int)ShipmentStatus.Created;
                serviceModel.ShipmentType = (int)ShipmentType.Package;

                int shiptmentId = _shipmentService.AddShipment(serviceModel);
                _packageInfoService.AddInfo(shiptmentId, model.VolumetricWeight);
                return shiptmentId;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
