using FleetManagement.Application.DTO;
using FleetManagement.Application.Enums;
using FleetManagement.Application.Interfaces;
using FleetManagement.Application.Services;
using FleetManagement.Common;
using FleetManagement.Infrastructure.Entities;
using FleetManagement.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace FleetManagement.Test
{
    public class ShipmentServiceTest
    {
        private readonly Mock<IShipmentRepository> _shipmentRepository;
        private readonly Mock<IDeliveryPointService> _deliveryPointService;
        private readonly Mock<ILogger<ShipmentService>> _logger;

        private readonly ShipmentService _shipmentService;

        public ShipmentServiceTest()
        {
            _shipmentRepository = new Mock<IShipmentRepository>();
            _deliveryPointService = new Mock<IDeliveryPointService>();
            _logger = new Mock<ILogger<ShipmentService>>();

            _shipmentService = new ShipmentService(_shipmentRepository.Object, _deliveryPointService.Object, _logger.Object);
        }

        [Fact]
        public void Be_able_to_add_PackageServiceModel()
        {
            ShipmentServiceModel model = new ShipmentServiceModel();

            model.Barcode = "PC54568";
            model.DeliveryPointForUnloading = 1;
            model.Status = (int)ShipmentStatus.Created;
            model.ShipmentType = (int)ShipmentType.Bag;

            _deliveryPointService.Setup(c => c.HasDeliveryPointByType(1)).Returns(true);

            Assert.IsNotType<Exception>(_shipmentService.AddShipment(model));
        }

        [Fact]
        public void Adding_Shipment_ShipmentServiceModel_Should_Return_DataNotFoundException()
        {
            ShipmentServiceModel model = new ShipmentServiceModel();

            model.Barcode = "PC54568";
            model.DeliveryPointForUnloading = 999;
            model.Status = (int)ShipmentStatus.Created;
            model.ShipmentType = (int)ShipmentType.Bag;

            _deliveryPointService.Setup(c => c.HasDeliveryPointByType(1)).Returns(false);

            Assert.Throws<DataNotFoundException>(() => { _shipmentService.AddShipment(model); });
        }

        [Fact]
        public void Adding_Shipment_ShipmentServiceModel_Should_Return_DuplicateDataException()
        {
            ShipmentServiceModel model = new ShipmentServiceModel();

            model.Barcode = "PC54568";
            model.DeliveryPointForUnloading = (int)DeliveryPointType.Branch;
            model.Status = (int)ShipmentStatus.Created;
            model.ShipmentType = (int)ShipmentType.Bag;

            _deliveryPointService.Setup(c => c.HasDeliveryPointByType(1)).Returns(true);
            _shipmentRepository.Setup(c => c.HasShipment("PC54568")).Returns(true);

            Assert.Throws<DuplicateDataException>(() => { _shipmentService.AddShipment(model); });
        }

        [Fact]
        public void GetByBarcodeAndDeliveryPointType_And_Barcode_deliveryPointType_Should_Return_ShipmentServiceModel()
        {
            _shipmentRepository.Setup(c => c.GetByBarcodeAndDeliveryPointType("PC54568", (int)DeliveryPointType.Branch)).Returns(shipmentEntityTestModel);

            ShipmentServiceModel serviceModel = _shipmentService.GetByBarcodeAndDeliveryPointType("PC54568", (int)DeliveryPointType.Branch);

            Assert.IsType<ShipmentServiceModel>(serviceModel);
        }

        [Fact]
        public void Be_able_to_UpdateStatus_Barcode_ShipmentStatus()
        {
            try
            {
                _shipmentService.UpdateStatus("PC54568", ShipmentStatus.Loaded);
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
        public void Be_able_to_UpdateStatus_Barcodes_ShipmentStatus()
        {
            try
            {
                _shipmentService.UpdateStatus(new List<string> { "PC54568" }, ShipmentStatus.Loaded);
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
        public void HasShipment_And_Barcode_Should_Return_Boolean()
        {
            Assert.IsType<Boolean>(_shipmentService.HasShipment("PC54568"));
        }

        [Fact]
        public void GetByBarcode_And_Barcode_Should_Return_DataNotFoundException()
        {
            Assert.Throws<DataNotFoundException>(() => { _shipmentService.GetByBarcode("PC54568"); });
        }

        [Fact]
        public void GetByBarcode_And_Barcode_Should_Return_ShipmentServiceModel()
        {
            _shipmentRepository.Setup(c => c.GetByBarcode("PC54568")).Returns(shipmentEntityTestModel);
            Assert.IsType<ShipmentServiceModel>(_shipmentService.GetByBarcode("PC54568"));
        }

        [Fact]
        public void GetShipmentBarcodes_And_Barcode_Should_Return_Barcodes()
        {
            _shipmentRepository.Setup(c => c.GetShipments(new List<string> { "PC54568" })).Returns(shipmentEntityTestModels);

            Assert.IsType<List<string>>(_shipmentService.GetShipmentBarcodes(new List<string> { "PC54568" }));
        }

        private ShipmentEntity shipmentEntityTestModel()
        {
            return new ShipmentEntity
            {
                Barcode = "",
                CreatedDate = DateTime.Now,
                DeliveryPointForUnloading = 1,
                ShipmentType = 1,
                Status = 1
            };
        }

        private List<ShipmentEntity> shipmentEntityTestModels()
        {
            return new List<ShipmentEntity>
            {
                new ShipmentEntity
                {
                    Barcode = "",
                    CreatedDate = DateTime.Now,
                    DeliveryPointForUnloading = 1,
                    ShipmentType = 1,
                    Status = 1
                }
            };
        }
    }
}
