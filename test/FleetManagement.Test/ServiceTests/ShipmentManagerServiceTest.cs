using FleetManagement.Application.DTO;
using FleetManagement.Application.Interfaces;
using FleetManagement.Application.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FleetManagement.Test
{
    public class ShipmentManagerServiceTest
    {
        private readonly Mock<IShipmentService> _shipmentService;
        private readonly Mock<IAssignmentService> _assignmentService;
        private readonly Mock<ILogger<ShipmentManager>> _logger;

        private readonly ShipmentManager _shipmentManager;

        public ShipmentManagerServiceTest()
        {
            _shipmentService = new Mock<IShipmentService>();
            _assignmentService = new Mock<IAssignmentService>();
            _logger = new Mock<ILogger<ShipmentManager>>();
            _shipmentManager = new ShipmentManager(_shipmentService.Object, _assignmentService.Object, _logger.Object);
        }

        [Fact]
        public void Adding_LicencePlate_34_TL_34_Should_Return_Ok()
        {
            try
            {
                _assignmentService.Setup(c => c.GetByBagBarcodes("PC54568")).Returns(assignmentSeviceModels);
                _assignmentService.Setup(c => c.GetByPackageBarcodes("PC54568")).Returns(assignmentSeviceModels);
                _shipmentService.Setup(c => c.GetByBarcode("PC54568")).Returns(shipmentServiceModel);

                _shipmentManager.ProcessShipment("PC54568");
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

        private List<AssignmentSeviceModel> assignmentSeviceModels()
        {
            return new List<AssignmentSeviceModel>
            {
                new AssignmentSeviceModel
                {
                   BagBarcode = "",
                   PackageBarcode = ""
                }
            };
        }

        private ShipmentServiceModel shipmentServiceModel()
        {
            return new ShipmentServiceModel
            {
                Barcode = "",
                DeliveryPointForUnloading = 1,
                ShipmentType = 1,
                Status = 1
            };
        }
    }
}
