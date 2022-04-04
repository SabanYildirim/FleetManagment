using FleetManagement.Application.DTO;
using FleetManagement.Application.Enums;
using FleetManagement.Application.Interfaces;
using FleetManagement.Application.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FleetManagement.Test
{
    public class ShipmentDeliveryPointValidatorTest
    {
        private readonly Mock<IShipmentService> _shipmentService;
        private readonly Mock<IAssignmentService> _assignmentService;

        private readonly BranchDeliveryPointValidator _branchDeliveryPointValidator;
        private readonly DistributionCenterPointValidator _distributionDeliveryPointValidator;
        private readonly TransferCenterDeliveryPointValidator _transferCenterDeliveryPointValidator;

        public ShipmentDeliveryPointValidatorTest()
        {
            _shipmentService = new Mock<IShipmentService>();
            _assignmentService = new Mock<IAssignmentService>();

            _branchDeliveryPointValidator = new BranchDeliveryPointValidator(_shipmentService.Object);
            _distributionDeliveryPointValidator = new DistributionCenterPointValidator(_shipmentService.Object);
            _transferCenterDeliveryPointValidator = new TransferCenterDeliveryPointValidator(_shipmentService.Object,_assignmentService.Object);

        }

        [Fact]
        public void BranchDeliveryPoint_And_Barcodes_Should_return_Valid_Barcodes()
        {

            _shipmentService.Setup(c => c.GetByBarcodeAndDeliveryPointType("P7988000121", (int)DeliveryPointType.Branch)).Returns(new ShipmentServiceModel
            {
                Barcode = "P7988000121",
                ShipmentType = (int)ShipmentType.Package
            });

            _shipmentService.Setup(c => c.GetByBarcodeAndDeliveryPointType("C725799", (int)DeliveryPointType.Branch)).Returns(new ShipmentServiceModel
            {
                Barcode = "C725799",
                ShipmentType = (int)ShipmentType.Bag
            });

            List<string> borcodes = new List<string>();
            borcodes.Add("P7988000121");
            borcodes.Add("C725799");

            var branchDeliveryPointValidBarcodes = _branchDeliveryPointValidator.getValidShipments(borcodes);

            Assert.Collection(branchDeliveryPointValidBarcodes,
                  item => item.Contains("P7988000121"));

            Assert.True(branchDeliveryPointValidBarcodes.Count == 1);
        }

        [Fact]
        public void DistributionCenter_And_Barcodes_Should_Return_Valid_Barcodes()
        {

            _shipmentService.Setup(c => c.GetByBarcodeAndDeliveryPointType("P7988000121", (int)DeliveryPointType.DistributionCenter)).Returns(new ShipmentServiceModel
            {
                Barcode = "P7988000121",
                ShipmentType = (int)ShipmentType.Package
            });

            _shipmentService.Setup(c => c.GetByBarcodeAndDeliveryPointType("C725799", (int)DeliveryPointType.TransferCenter)).Returns(new ShipmentServiceModel
            {
                Barcode = "C725799",
                ShipmentType = (int)ShipmentType.Bag,
            });

            List<string> borcodes = new List<string>();
            borcodes.Add("P7988000121");
            borcodes.Add("C725799");

            var distributionDeliveryPointValidBarcodes = _distributionDeliveryPointValidator.getValidShipments(borcodes);

            Assert.True(!distributionDeliveryPointValidBarcodes.Contains("C725799"));
            Assert.True(distributionDeliveryPointValidBarcodes.Count == 1);
        }

        [Fact]
        public void TransferCenter_And_Barcodes_Should_Return_Valid_Barcodes()
        {
            _shipmentService.Setup(c => c.GetByBarcodeAndDeliveryPointType("P7988000121", (int)DeliveryPointType.TransferCenter)).Returns(new ShipmentServiceModel
            {
                Barcode = "P7988000121",
                ShipmentType = (int)ShipmentType.Package
            });

            _shipmentService.Setup(c => c.GetByBarcodeAndDeliveryPointType("C725799", (int)DeliveryPointType.TransferCenter)).Returns(new ShipmentServiceModel
            {
                Barcode = "C725799",
                ShipmentType = (int)ShipmentType.Bag,
            });

            _assignmentService.Setup(c => c.HasAssigment("P7988000121")).Returns(true);
            _assignmentService.Setup(c => c.HasAssigment("C725799")).Returns(false);

            List<string> borcodes = new List<string>();
            borcodes.Add("P7988000121");
            borcodes.Add("C725799");

            var transferCenterPointValidBarcodes = _transferCenterDeliveryPointValidator.getValidShipments(borcodes);

            Assert.True(!transferCenterPointValidBarcodes.Contains("C725799"));
            Assert.True(transferCenterPointValidBarcodes.Count == 1);
        }
    }
}
