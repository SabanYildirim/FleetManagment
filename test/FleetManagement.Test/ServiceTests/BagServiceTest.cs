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
    public class BagServiceTest
    {
        private readonly BagService _bagService;
        private readonly Mock<IDeliveryPointService> _deliveryPointService;
        private readonly Mock<IShipmentService> _shipmentService;
        private readonly Mock<ILogger<BagService>> _logger;

        public BagServiceTest()
        {
            _deliveryPointService = new Mock<IDeliveryPointService>();
            _shipmentService = new Mock<IShipmentService>();
            _logger = new Mock<ILogger<BagService>>();

            _bagService = new BagService(_deliveryPointService.Object, _shipmentService.Object, _logger.Object);
        }

        [Fact]
        public void Be_able_to_add_BagServiceModel()
        {
            BagServiceModel model = new BagServiceModel();
            model.Barcode = "C725799";
            model.DeliveryPointforUnloading = (int)DeliveryPointType.Branch;

            Assert.IsNotType<Exception>(_bagService.AddBag(model));
        }
    }
}
