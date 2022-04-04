using FleetManagement.Infrastructure.Context;
using FleetManagement.Infrastructure.Entities;
using FleetManagement.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FleetManagement.Infrastructure.Repositories
{
    public class DeliveryPointRepository : IDeliveryPointRepository
    {
        private readonly FleetManagementContext _fleetManagementContext;

        public DeliveryPointRepository(FleetManagementContext fleetManagementContext)
        {
            _fleetManagementContext = fleetManagementContext;
        }

        public int Add(DeliveryPointEntity entity)
        {
            _fleetManagementContext.DeliveryPointies.Add(entity);
            _fleetManagementContext.SaveChanges();
            return entity.Id;
        }

        public void Delete(DeliveryPointEntity entity)
        {
            throw new NotImplementedException();
        }

        public DeliveryPointEntity Get(long id)
        {
            return _fleetManagementContext.DeliveryPointies.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<DeliveryPointEntity> GetAll()
        {
            return _fleetManagementContext.DeliveryPointies.ToList();
        }

        public void Update(DeliveryPointEntity dbEntity, DeliveryPointEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool HasDeliveryPointByType(int type)
        {
            return _fleetManagementContext.DeliveryPointies.Any(e => e.Type == type);
        }

        public int GetDeliveryPointType(int type)
        {
            var deliveryPoint = _fleetManagementContext.DeliveryPointies.FirstOrDefault(x => x.Type == type);
            if (deliveryPoint != null)
                return deliveryPoint.Type;

            return 0;
        }
    }
}
