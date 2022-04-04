using FleetManagement.Application.DTO;
using FleetManagement.Application.Enums;
using FleetManagement.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Application.Interfaces
{
    public interface IDeliveryPointService
    {
        void AddDeliveryPoint(DeliveryPointServiceModel model);
        bool HasDeliveryPointByType(int type);
        DeliveryPointType GetDeliveryPointType(int typeId);
    }
}
