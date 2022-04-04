using FleetManagement.Application.DTO;
using FleetManagement.Application.DTO.request;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace FleetManagement.Test.ControllerTests
{
    [Collection("C1"), Order(4)]
    public class DPackageControllerTest : IClassFixture<WebApplicationFactory<Api.Startup>>
    {
        [Fact]
        public async Task Add_Package_Barcode_Branch_Should_Return_HttpStatusCode_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("api/package"
                        , new StringContent(
                        JsonConvert.SerializeObject(new PackagesRequestModel()
                        {
                            Barcode = "P7988000121",
                            DeliveryPointforUnloading = 1,
                            VolumetricWeight = 5
                        }),
                    Encoding.UTF8,
                    "application/json"));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Add_Package_Barcode_Distribution_Should_Return_HttpStatusCode_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("api/package"
                        , new StringContent(
                        JsonConvert.SerializeObject(new PackagesRequestModel()
                        {
                            Barcode = "P8988000120",
                            DeliveryPointforUnloading = 2,
                            VolumetricWeight = 5
                        }),
                    Encoding.UTF8,
                    "application/json"));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Add_Package_Barcode_Transfer_Should_Return_HttpStatusCode_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("api/package"
                        , new StringContent(
                        JsonConvert.SerializeObject(new PackagesRequestModel()
                        {
                            Barcode = "P9988000126",
                            DeliveryPointforUnloading = 3,
                            VolumetricWeight = 5
                        }),
                    Encoding.UTF8,
                    "application/json"));

                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Package_DeliveryPointforUnloading_Empty_Should_Return_ValidationException()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/package"
                        , new StringContent(
                        JsonConvert.SerializeObject(new PackagesRequestModel()
                        {
                            Barcode = "P8988000122",
                            DeliveryPointforUnloading = 0,
                            VolumetricWeight = 10
                        }),
                    Encoding.UTF8,
                    "application/json"));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task Package_Barcode_Empty_Should_Return_ValidationException()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/package"
                        , new StringContent(
                        JsonConvert.SerializeObject(new PackagesRequestModel()
                        {
                            Barcode = "",
                            DeliveryPointforUnloading = 1,
                            VolumetricWeight = 10
                        }),
                    Encoding.UTF8,
                    "application/json"));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task Package_VolumetricWeight_Empty_Should_Return_ValidationException()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/package"
                        , new StringContent(
                        JsonConvert.SerializeObject(new PackagesRequestModel()
                        {
                            Barcode = "P8988000122",
                            DeliveryPointforUnloading = 1,
                            VolumetricWeight = 0
                        }),
                    Encoding.UTF8,
                    "application/json"));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }
    }
}
