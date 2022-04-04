using FleetManagement.Infrastructure.Context;
using FleetManagement.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FleetManagement.Infrastructure.Repositories
{
    public class LicensePlateRepository : ILicensePlateRepository
    {
        private readonly FleetManagementContext _fleetManagementContext;

        public LicensePlateRepository(FleetManagementContext fleetManagementContext)
        {
            _fleetManagementContext = fleetManagementContext;
        }

        public int Add(LicensePlateEntity entity)
        {
            _fleetManagementContext.LicensePlates.Add(entity);
            _fleetManagementContext.SaveChanges();
            return entity.Id;
        }

        public void Delete(LicensePlateEntity entity)
        {
            throw new NotImplementedException();
        }

        public LicensePlateEntity Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LicensePlateEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(LicensePlateEntity dbEntity, LicensePlateEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool HasPlate(string plate)
        {
            return _fleetManagementContext.LicensePlates.Any(x => x.Plate == plate);
        }
    }
}
