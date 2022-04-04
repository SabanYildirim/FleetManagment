using FleetManagement.Application.DTO;
using FleetManagement.Application.Services;
using FleetManagement.Common;
using FleetManagement.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FleetManagement.Test
{
    public class DeliveryPointServiceTest
    {
        private readonly DeliveryPointService _deliveryPointService;
        private readonly Mock<IDeliveryPointRepository> _deliveryPointRepository;
        private readonly Mock<ILogger<DeliveryPointService>> _logger;

        public DeliveryPointServiceTest()
        {
            _deliveryPointRepository = new Mock<IDeliveryPointRepository>();
            _logger = new Mock<ILogger<DeliveryPointService>>();

            _deliveryPointService = new DeliveryPointService(_deliveryPointRepository.Object, _logger.Object);
        }

        [Fact]
        public void Be_able_to_Add_DeliveryPoint_DeliveryPointServiceModel()
        {
            try
            {
                DeliveryPointServiceModel model = new DeliveryPointServiceModel();
                model.DeliveryPointName = "testBranch";
                model.Value = 99;

                _deliveryPointRepository.Setup(c => c.HasDeliveryPointByType(1)).Returns(false);

                _deliveryPointService.AddDeliveryPoint(model);
            }
            catch (Exception ex)
            {
                Assert.False(true);
            }
            finally
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void Be_able_to_Add_DeliveryPoint_Should_Return_DuplicateDataException()
        {
            DeliveryPointServiceModel model = new DeliveryPointServiceModel();
            model.DeliveryPointName = "Branch";
            model.Value = 1;

            _deliveryPointRepository.Setup(c => c.HasDeliveryPointByType(1)).Returns(true);
            
            Assert.Throws<DuplicateDataException>(() => { _deliveryPointService.AddDeliveryPoint(model); });
        }
    }
}
