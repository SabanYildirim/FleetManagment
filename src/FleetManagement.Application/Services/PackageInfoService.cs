using FleetManagement.Application.Interfaces;
using FleetManagement.Infrastructure.Entities;
using FleetManagement.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace FleetManagement.Application.Services
{
    public class PackageInfoService : IPackageInfoService
    {
        private readonly IPackageInfoRepository _packageInfoRepository;
        private readonly ILogger<PackageInfoService> _logger;

        public PackageInfoService(
            IPackageInfoRepository volumetricWeightRepository,
            ILogger<PackageInfoService> logger)
        {
            _packageInfoRepository = volumetricWeightRepository;
            _logger = logger;
        }

        public void AddInfo(int shiptmentId, int weight)
        {
            try
            {
                PackageInfo entity = new PackageInfo();
                entity.ShipmentId = shiptmentId;
                entity.Weight = weight;

                _packageInfoRepository.Add(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                throw;
            }
        }
    }
}
