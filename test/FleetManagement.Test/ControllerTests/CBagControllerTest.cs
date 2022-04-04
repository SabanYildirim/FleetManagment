using FleetManagement.Application.DTO;
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
    [Collection("C1"), Order(3)]
    public class CBagControllerTest : IClassFixture<WebApplicationFactory<Api.Startup>>
    {
        [Fact, Order(5)]
        public async Task Add_Bag_Barcode_Transfer_Center_Should_Return_HttpStatusCode_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("api/bag"
                        , new StringContent(
                        JsonConvert.SerializeObject(new BagRequestModel()
                        {
                            Barcode = "C72579923",
                            DeliveryPointforUnloading = 1
                        }),
                    Encoding.UTF8,
                    "application/json"));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact, Order(4)]
        public async Task Add_Bag_Barcode_Distribution_Center_Should_Return_HttpStatusCode_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("api/bag"
                        , new StringContent(
                        JsonConvert.SerializeObject(new BagRequestModel()
                        {
                            Barcode = "C725799",
                            DeliveryPointforUnloading = 2
                        }),
                    Encoding.UTF8,
                    "application/json"));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact, Order(3)]
        public async Task Add_Bag_Barcode_Should_Return_HttpStatusCode_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("api/bag"
                        , new StringContent(
                        JsonConvert.SerializeObject(new BagRequestModel()
                        {
                            Barcode = "C725800",
                            DeliveryPointforUnloading = 3
                        }),
                    Encoding.UTF8,
                    "application/json"));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact, Order(2)]
        public async Task Bag_DeliveryPointforUnloading_Empty_Should_Return_ValidationException()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/bag"
                        , new StringContent(
                        JsonConvert.SerializeObject(new BagRequestModel()
                        {
                            Barcode = "C725799",
                            DeliveryPointforUnloading = 0
                        }),
                    Encoding.UTF8,
                    "application/json"));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact, Order(1)]
        public async Task Bag_Barcode_Empty_Should_Return_ValidationException()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/bag"
                        , new StringContent(
                        JsonConvert.SerializeObject(new BagRequestModel()
                        {
                            Barcode = "",
                            DeliveryPointforUnloading = 1
                        }),
                    Encoding.UTF8,
                    "application/json"));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }
    }
}
