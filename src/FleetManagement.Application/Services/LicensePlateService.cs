using FleetManagement.Application.Interfaces;
using FleetManagement.Common;
using FleetManagement.Infrastructure;
using FleetManagement.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace FleetManagement.Application.Services
{
    public class LicensePlateService : ILicensePlateService
    {
        private readonly ILicensePlateRepository _licensePlateRepository;
        private readonly ILogger<LicensePlateService> _logger;

        public LicensePlateService(
            ILicensePlateRepository licensePlateRepository,
            ILogger<LicensePlateService> logger
            )
        {
            _licensePlateRepository = licensePlateRepository;
            _logger = logger;
        }

        public void AddPlate(string plate)
        {
            try
            {
                if (HasPlate(plate))
                {
                    throw new DuplicateDataException(AppErrorCodeConstants.PlateFoundErrorCode, "Plate already exist");
                }

                LicensePlateEntity entity = new LicensePlateEntity();
                entity.Plate = plate;

                _licensePlateRepository.Add(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public bool HasPlate(string plate)
        {
            return _licensePlateRepository.HasPlate(plate);
        }
    }
}
