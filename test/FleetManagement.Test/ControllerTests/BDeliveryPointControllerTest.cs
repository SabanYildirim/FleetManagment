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
    [Collection("C1"), Order(2)]
    public class BDeliveryPointControllerTest : IClassFixture<WebApplicationFactory<Api.Startup>>
    {
        [Fact]
        public async Task Add_Branch_DeliveryPoint_Should_Return_HttpStatusCode_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("api/deliverypoint"
                        , new StringContent(
                        JsonConvert.SerializeObject(new DeliveryPointRequestModel()
                        {
                             DeliveryPointName = "Branch",
                             Value = 1
                        }),
                    Encoding.UTF8,
                    "application/json"));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Add_Distribution_Center_DeliveryPoint_Should_Return_HttpStatusCode_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("api/deliverypoint"
                        , new StringContent(
                        JsonConvert.SerializeObject(new DeliveryPointRequestModel()
                        {
                            DeliveryPointName = "Distribution Center",
                            Value = 2
                        }),
                    Encoding.UTF8,
                    "application/json"));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Add_Transfer_Center_DeliveryPoint_Should_Return_HttpStatusCode_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("api/deliverypoint"
                        , new StringContent(
                        JsonConvert.SerializeObject(new DeliveryPointRequestModel()
                        {
                            DeliveryPointName = "Transfer Center",
                            Value = 3
                        }),
                    Encoding.UTF8,
                    "application/json"));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task DeliveryPoint_Name_Empty_Should_Return_ValidationException()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("api/deliverypoint"
                        , new StringContent(
                        JsonConvert.SerializeObject(new DeliveryPointRequestModel()
                        {
                            DeliveryPointName = "",
                            Value = 10
                        }),
                    Encoding.UTF8,
                    "application/json"));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task DeliveryPoint_Value_Empty_Should_Return_ValidationException()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/assignment"
                        , new StringContent(
                        JsonConvert.SerializeObject(new DeliveryPointRequestModel()
                        {
                            DeliveryPointName = "Branch",
                            Value = 0
                        }),
                    Encoding.UTF8,
                    "application/json"));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }
    }
}
