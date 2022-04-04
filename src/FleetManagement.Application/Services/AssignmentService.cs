using FleetManagement.Application.Interfaces;
using FleetManagement.Infrastructure.Entities;
using FleetManagement.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;
using FleetManagement.Common;
using System.Net;
using FleetManagement.Application.Enums;
using FleetManagement.Application.DTO;
using Microsoft.Extensions.Logging;
using System;
using FleetManagement.Common.Exceptions;

namespace FleetManagement.Application.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IShipmentService _shipmentService;
        private readonly ILogger<AssignmentService> _logger;

        public AssignmentService(IAssignmentRepository assignmentRepository,
            IShipmentService shipmentService,
            ILogger<AssignmentService> logger
            )
        {
            _assignmentRepository = assignmentRepository;
            _shipmentService = shipmentService;
            _logger = logger;
        }

        public bool HasAssigment(string barcode)
        {
            try
            {
                return _assignmentRepository.HasAssignment(barcode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public void Assign(string packageBarcode, string bagBarcode)
        {
            try
            {
                ValidateAssignment(packageBarcode,bagBarcode);

                AssignmentEntity entity = new AssignmentEntity();
                entity.PackageBarcode = packageBarcode;
                entity.BagBarcode = bagBarcode;

                _assignmentRepository.Add(entity);

                _shipmentService.UpdateStatus(packageBarcode, ShipmentStatus.LoadedIntoBag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<AssignmentSeviceModel> GetByBagBarcodes(string barcode)
        {
            try
            {
                List<AssignmentSeviceModel> assignmentSeviceModels = new List<AssignmentSeviceModel>();

                var bagBarcodesEntity = _assignmentRepository.GetByBagBarcodes(barcode);
                if (bagBarcodesEntity == null || !bagBarcodesEntity.Any()) 
                {
                    _logger.LogInformation(AppErrorCodeConstants.PackageNotFoundErrorCode, $"Bag not found ({barcode})");
                    return null;
                }
          
                foreach (var bagBarcodeEntity in bagBarcodesEntity)
                {
                    AssignmentSeviceModel model = new AssignmentSeviceModel();

                    model.BagBarcode = bagBarcodeEntity.BagBarcode;
                    model.PackageBarcode = bagBarcodeEntity.PackageBarcode;
                    assignmentSeviceModels.Add(model);
                }

                return assignmentSeviceModels;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<AssignmentSeviceModel> GetByPackageBarcodes(string barcode)
        {
            try
            {
                List<AssignmentSeviceModel> assignmentsSeviceModel = new List<AssignmentSeviceModel>();

                var packageBarcodesEntity = _assignmentRepository.GetByPackageBarcodes(barcode);
                if (packageBarcodesEntity == null || !packageBarcodesEntity.Any()) 
                {
                    _logger.LogInformation(AppErrorCodeConstants.PackageNotFoundErrorCode, $"Package not found ({barcode})");
                    return null;
                }

                foreach (var packageBarcodeEntity in packageBarcodesEntity)
                {
                    AssignmentSeviceModel model = new AssignmentSeviceModel();
                    model.BagBarcode = packageBarcodeEntity.BagBarcode;
                    model.PackageBarcode = packageBarcodeEntity.PackageBarcode;

                    assignmentsSeviceModel.Add(model);
                }

                return assignmentsSeviceModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private void ValidateAssignment(string packageBarcode, string bagBarcode)
        {
            if (!_shipmentService.HasShipment(bagBarcode))
            {
                throw new DataNotFoundException(AppErrorCodeConstants.BagNotFoundErrorCode, $"Bag not found. ({bagBarcode})");
            }
            if (!_shipmentService.HasShipment(packageBarcode))
            {
                throw new DataNotFoundException(AppErrorCodeConstants.PackageNotFoundErrorCode, $"Package not found. ({packageBarcode})");
            }
            if (_shipmentService.GetByBarcode(bagBarcode).DeliveryPointForUnloading !=
                _shipmentService.GetByBarcode(packageBarcode).DeliveryPointForUnloading)
            {
                throw new ShipmentRouteException(AppErrorCodeConstants.ShipmentRouteErrorCode, $"Shipment route is wrong");
            }
            else if (HasAssigment(packageBarcode))
            {
                throw new DuplicateDataException(AppErrorCodeConstants.PackageAssigmentFoundErrorCode, $"Package assigment found ({packageBarcode})");
            }
        }
    }
}
