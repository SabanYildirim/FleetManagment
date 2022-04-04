using FleetManagement.Application.DTO;
using FleetManagement.Application.Enums;
using FleetManagement.Application.Interfaces;
using FleetManagement.Application.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace FleetManagement.Test
{
    public class PackageServiceTest
    {
        private readonly PackageService _packageService;

        private readonly Mock<IDeliveryPointService> _deliveryPointService;
        private readonly Mock<IShipmentService> _shipmentService;
        private readonly Mock<ILogger<PackageService>> _logger;
        private readonly Mock<IPackageInfoService> _packageInfoService;

        public PackageServiceTest()
        {
            _deliveryPointService = new Mock<IDeliveryPointService>();
            _shipmentService = new Mock<IShipmentService>();
            _logger = new Mock<ILogger<PackageService>>();
            _packageInfoService = new Mock<IPackageInfoService>();

            _packageService = new PackageService(_deliveryPointService.Object, _shipmentService.Object, _packageInfoService.Object, _logger.Object);
        }

        [Fact]
        public void Be_able_to_add_PackageServiceModel()
        {
            PackageServiceModel model = new PackageServiceModel();
            model.Barcode = "P7988000121";
            model.DeliveryPointforUnloading = (int)DeliveryPointType.Branch;
            model.VolumetricWeight = 0;

            Assert.IsNotType<Exception>( _packageService.AddPackage(model) );
        }
    }
}
