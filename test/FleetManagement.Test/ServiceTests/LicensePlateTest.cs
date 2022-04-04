using FleetManagement.Application.Services;
using FleetManagement.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace FleetManagement.Test
{
    public class LicensePlateTest
    {
        private readonly Mock<ILicensePlateRepository> _licensePlateRepository;
        private readonly Mock<ILogger<LicensePlateService>> _logger;

        private readonly LicensePlateService _licensePlateService;

        public LicensePlateTest()
        {
            _licensePlateRepository = new Mock<ILicensePlateRepository>();
            _logger = new Mock<ILogger<LicensePlateService>>();

            _licensePlateService = new LicensePlateService(_licensePlateRepository.Object,_logger.Object);
        }

        [Fact]
        public void Adding_LicencePlate_34_TL_34_Should_Return_Ok()
        {
            try
            {
                _licensePlateService.AddPlate("34 TL 34");
            }
            catch (Exception ex)
            {
                Assert.False(true);
            }
            finally
            {
                Assert.True(true);
            }
        }
    }
}
