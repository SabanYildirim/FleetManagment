using FleetManagement.Application.DTO;
using FleetManagement.Application.Enums;
using FleetManagement.Application.Interfaces;
using FleetManagement.Common;
using FleetManagement.Infrastructure.Entities;
using FleetManagement.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FleetManagement.Application.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IDeliveryPointService _deliveryPointService;
        private readonly ILogger<ShipmentService> _logger;

        public ShipmentService(
            IShipmentRepository shipmentRepository,
            IDeliveryPointService deliveryPointService,
            ILogger<ShipmentService> logger)
        {
            _shipmentRepository = shipmentRepository;
            _deliveryPointService = deliveryPointService;
            _logger = logger;
        }

        public int AddShipment(ShipmentServiceModel shipmentModel)
        {
            try
            {
                if (!_deliveryPointService.HasDeliveryPointByType(shipmentModel.DeliveryPointForUnloading))
                {
                    throw new DataNotFoundException(AppErrorCodeConstants.DeliveryPointNotFoundErrorCode, $"Delivery not found ({shipmentModel.DeliveryPointForUnloading})");
                }

                if (HasShipment(shipmentModel.Barcode))
                {
                    throw new DuplicateDataException(AppErrorCodeConstants.PackageFoundErrorCode, $"Barcode already exist ({shipmentModel.Barcode})");
                }

                ShipmentEntity entity = new ShipmentEntity();
                entity.Barcode = shipmentModel.Barcode;
                entity.DeliveryPointForUnloading = shipmentModel.DeliveryPointForUnloading;
                entity.ShipmentType = shipmentModel.ShipmentType;
                entity.Status = shipmentModel.Status;

                return _shipmentRepository.Add(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        
        public ShipmentServiceModel GetByBarcodeAndDeliveryPointType(string barcode, int deliveryPointType)
        {
            try
            {
                var entity = _shipmentRepository.GetByBarcodeAndDeliveryPointType(barcode, deliveryPointType);
                if (entity == null)
                {
                    _logger.LogInformation(AppErrorCodeConstants.ShipmentNotFoundCode, $"Shipment not found ({barcode})");
                    return null;
                }

                ShipmentServiceModel serviceModel = new ShipmentServiceModel();
                serviceModel.Barcode = entity.Barcode;
                serviceModel.DeliveryPointForUnloading = entity.DeliveryPointForUnloading;
                serviceModel.ShipmentType = entity.ShipmentType;
                serviceModel.Status = entity.Status;

                return serviceModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public void UpdateStatus(string barcode, ShipmentStatus status)
        {
            UpdateStatus(new List<string>() { barcode }, status);
        }

        public void UpdateStatus(List<string> barcodes, ShipmentStatus status)
        {
            try
            {
                _shipmentRepository.UpdateStatus(barcodes, (int)status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public bool HasShipment(string barcode)
        {
            return _shipmentRepository.HasShipment(barcode);
        }

        public ShipmentServiceModel GetByBarcode(string barcode)
        {
            try
            {
                var entity = _shipmentRepository.GetByBarcode(barcode);
                if (entity == null)
                    throw new DataNotFoundException(AppErrorCodeConstants.ShipmentNotFoundCode, $"Shipment not found ({barcode})");

                ShipmentServiceModel serviceModel = new ShipmentServiceModel();
                serviceModel.Barcode = entity.Barcode;
                serviceModel.DeliveryPointForUnloading = entity.DeliveryPointForUnloading;
                serviceModel.ShipmentType = entity.ShipmentType;
                serviceModel.Status = entity.Status;
                return serviceModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public List<string> GetShipmentBarcodes(List<string> barcodes)
        {
            return  _shipmentRepository.GetShipments(barcodes)?.Select(s => s.Barcode).ToList();
        }
    }
}
