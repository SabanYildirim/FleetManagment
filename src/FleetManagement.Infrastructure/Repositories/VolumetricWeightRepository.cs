using FleetManagement.Infrastructure.Context;
using FleetManagement.Infrastructure.Entities;
using FleetManagement.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Infrastructure.Repositories
{
    public class VolumetricWeightRepository : IPackageInfoRepository
    {
        private readonly FleetManagementContext _fleetManagementContext;
        public VolumetricWeightRepository(FleetManagementContext fleetManagementContext)
        {
            _fleetManagementContext = fleetManagementContext;
        }

        public int Add(PackageInfo entity)
        {
            _fleetManagementContext.VolumetricWeights.Add(entity);
            _fleetManagementContext.SaveChanges();
            return entity.Id;
        }

        public void Delete(PackageInfo entity)
        {
            throw new NotImplementedException();
        }

        public PackageInfo Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PackageInfo> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(PackageInfo dbEntity, PackageInfo entity)
        {
            throw new NotImplementedException();
        }
    }
}
