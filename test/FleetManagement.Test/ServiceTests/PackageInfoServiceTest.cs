using FleetManagement.Application.Services;
using FleetManagement.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FleetManagement.Test
{
    public class PackageInfoServiceTest
    {
        private readonly Mock<IPackageInfoRepository> _packageInfoRepository;
        private readonly Mock<ILogger<PackageInfoService>> _logger;

        private readonly PackageInfoService _packageInfoService;

        public PackageInfoServiceTest()
        {
            _packageInfoRepository = new Mock<IPackageInfoRepository>();
            _logger = new Mock<ILogger<PackageInfoService>>();

            _packageInfoService = new PackageInfoService(_packageInfoRepository.Object,_logger.Object);
        }

        [Fact]
        public void Be_able_to_AddPackageInfo_shipmentId_weight()
        {
            try
            {
                _packageInfoService.AddInfo(0,0);
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
