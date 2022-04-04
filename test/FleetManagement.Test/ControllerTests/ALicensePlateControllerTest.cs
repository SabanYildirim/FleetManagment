using FleetManagement.Common.Exceptions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace FleetManagement.Test.ControllerTests
{
    [Collection("C1"), Order(1)]
    public class ALicensePlateControllerTest : IClassFixture<WebApplicationFactory<FleetManagement.Api.Startup>>
    {
        [Fact]
        public async Task Add_License_Plate_34TL34_Should_Return_HttpStatusCode_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                StringContent httpContent = new StringContent(String.Empty);

                var response = await client.PostAsync("/api/licenseplate?licensePlate=34 TL 34", httpContent);
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Add_LicensePlate_Should_Return_ValidationException()
        {
            using (var client = new TestClientProvider().Client)
            {
                StringContent httpContent = new StringContent(String.Empty);

                var response = await client.PostAsync("/api/licenseplate?license", httpContent);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }
    }
}
