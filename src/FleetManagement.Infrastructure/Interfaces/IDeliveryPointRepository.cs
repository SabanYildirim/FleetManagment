using FleetManagement.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Infrastructure.Interfaces
{
    public interface IDeliveryPointRepository : IDataRepository<DeliveryPointEntity>
    {
        bool HasDeliveryPointByType(int type);
        int GetDeliveryPointType(int type);
    }
}
