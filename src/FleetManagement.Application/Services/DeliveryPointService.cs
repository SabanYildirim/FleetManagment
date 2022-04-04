using FleetManagement.Application.DTO;
using FleetManagement.Application.Interfaces;
using System;
using FleetManagement.Infrastructure.Interfaces;
using FleetManagement.Infrastructure.Entities;
using FleetManagement.Application.Enums;
using FleetManagement.Common;
using Microsoft.Extensions.Logging;

namespace FleetManagement.Application.Services
{
    public class DeliveryPointService : IDeliveryPointService
    {
        private readonly IDeliveryPointRepository _deliveryPointRepository;
        private readonly ILogger<DeliveryPointService> _logger;

        public DeliveryPointService(
            IDeliveryPointRepository deliveryPointRepository,
            ILogger<DeliveryPointService> logger)
        {
            _deliveryPointRepository = deliveryPointRepository;
            _logger = logger;
        }

        public void AddDeliveryPoint(DeliveryPointServiceModel model)
        {
            try
            {
                if (HasDeliveryPointByType(model.Value))
                {
                    throw new DuplicateDataException(AppErrorCodeConstants.DeliveryPointFoundErrorCode, "Delivery point already exist");
                }

                DeliveryPointEntity Entity = new DeliveryPointEntity();
                Entity.DeliveryPoint = model.DeliveryPointName;
                Entity.Type = model.Value;

                _deliveryPointRepository.Add(Entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public DeliveryPointType GetDeliveryPointType(int typeId)
        {
            try
            {
                if (!HasDeliveryPointByType(typeId))
                {
                    throw new DataNotFoundException(AppErrorCodeConstants.DeliveryPointNotFoundErrorCode, $"Delivery not found({typeId})");
                }
                return (DeliveryPointType)_deliveryPointRepository.GetDeliveryPointType(typeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public bool HasDeliveryPointByType(int type)
        {
            return _deliveryPointRepository.HasDeliveryPointByType(type);
        }
    }
}
