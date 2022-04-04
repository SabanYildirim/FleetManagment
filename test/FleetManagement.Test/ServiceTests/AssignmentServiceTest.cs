using FleetManagement.Application.DTO;
using FleetManagement.Application.Interfaces;
using FleetManagement.Application.Services;
using FleetManagement.Infrastructure.Entities;
using FleetManagement.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FleetManagement.Test
{
    public class AssignmentServiceTest
    {
        private readonly Mock<IAssignmentRepository> _assignmentRepository;
        private readonly Mock<IShipmentService> _shipmentService;
        private readonly Mock<ILogger<AssignmentService>> _logger;

        private readonly AssignmentService _assignmentService;

        public AssignmentServiceTest()
        {
            _assignmentRepository = new Mock<IAssignmentRepository>();
            _shipmentService = new Mock<IShipmentService>();
            _logger = new Mock<ILogger<AssignmentService>>();

            _assignmentService = new AssignmentService(_assignmentRepository.Object, _shipmentService.Object, _logger.Object);
        }

        [Fact]
        public void HasAssigment_And_Barcode_Should_Return_Boolean()
        {
            Assert.IsType<Boolean>(_assignmentService.HasAssigment("PC54568"));
        }

        [Fact]
        public void Be_able_to_Assign_PackBarcode_Bag_Barcode()
        {
            try
            {
                _shipmentService.Setup(c => c.HasShipment("test")).Returns(true);
                _shipmentService.Setup(c => c.GetByBarcode("test")).Returns(shipmentServiceModel);
                _assignmentRepository.Setup(c => c.HasAssignment("test")).Returns(false);

                _assignmentService.Assign("test", "test");
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
        public void GetByBagBarcodes_And_Barcode_Should_Return_AssignmentSeviceModels()
        {
            _assignmentRepository.Setup(c => c.GetByBagBarcodes("PC54568")).Returns(assignmentEntities);

            Assert.IsType<List<AssignmentSeviceModel>>(_assignmentService.GetByBagBarcodes("PC54568").ToList());
        }

        [Fact]
        public void GetByPackageBarcodes_And_Barcode_Should_Return_AssignmentSeviceModels()
        {
            _assignmentRepository.Setup(c => c.GetByPackageBarcodes("PC54568")).Returns(assignmentEntities);

            Assert.IsType<List<AssignmentSeviceModel>>(_assignmentService.GetByPackageBarcodes("PC54568").ToList());
        }

        private List<AssignmentEntity> assignmentEntities()
        {
            return new List<AssignmentEntity>
            {
                new AssignmentEntity
                {
                     BagBarcode = "",
                     PackageBarcode = "",
                     Id = 0
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
