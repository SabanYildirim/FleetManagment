using FleetManagement.Application.DTO.request;
using FleetManagement.Application.DTO.response;
using FleetManagement.Application.Enums;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace FleetManagement.Test.ControllerTests
{
    [Collection("C1"), Order(6)]
    public class FShipmentControllerTest : IClassFixture<WebApplicationFactory<Api.Startup>>
    {
        public FShipmentControllerTest()
        {
        }

        [Fact]
        public async Task Shipment_Branch_Delivery_Point_Should_Return_HttpStatusCode_Ok()
        {

            var deliveries = new List<Delivery>
                                       {
                                            new Delivery
                                           {
                                                barcode = "P7988000121",
                                           },
                                            new Delivery
                                           {
                                                barcode = "C725799",
                                           },
                                            new Delivery
                                           {
                                                barcode = "C72579923",
                                           }
                                       };

            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("api/shipment"
                        , new StringContent(
                        JsonConvert.SerializeObject(new ShipmentRequestModel()
                        {
                            plate = "34 TL 34",
                            route = new List<Route>
                             {
                                 new Route
                                 {
                                      deliveryPoint = (int)DeliveryPointType.Branch,
                                       deliveries = deliveries
                                 }
                             }
                        }),
                        Encoding.UTF8, "application/json"));

                var contents = await response.Content.ReadAsStringAsync();
                var responseModels = JsonConvert.DeserializeObject<ShipmentResponseModel>(contents);

                foreach (var route in responseModels.route)
                {
                    foreach (var delivery in route.deliveries)
                    {
                        if (delivery.barcode.Contains("P7988000121") && delivery.State == (int)ShipmentStatus.Unloaded)
                        {
                            Assert.True(true);
                        }
                        else if (delivery.barcode.Contains("C72579923") && delivery.State == (int)ShipmentStatus.Loaded)
                        {
                            Assert.True(true);
                        }
                        else if (delivery.barcode.Contains("C725799") && delivery.State == (int)ShipmentStatus.Loaded)
                        {
                            Assert.True(true);
                        }
                        else
                        {
                            Assert.True(false);
                        }
                    }
                }

                Assert.True(responseModels.route?.Where(x => x.deliveryPoint == (int)DeliveryPointType.Branch)?.FirstOrDefault()?.deliveries.Count(x => x.State == (int)ShipmentStatus.Loaded) == 2);
                Assert.True(responseModels.route?.Where(x => x.deliveryPoint == (int)DeliveryPointType.Branch)?.FirstOrDefault()?.deliveries.Count(x => x.State == (int)ShipmentStatus.Unloaded) == 1);

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Shipment_Distribution_Center_Delivery_Point_Should_Return_HttpStatusCode_Ok()
        {

            var deliveries = new List<Delivery>
                                       {
                                            new Delivery
                                           {
                                                barcode = "P8988000120",
                                           },
                                            new Delivery
                                           {
                                                barcode = "C725799",
                                           }
                                       };

            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("api/shipment"
                        , new StringContent(
                        JsonConvert.SerializeObject(new ShipmentRequestModel()
                        {
                            plate = "34 TL 34",
                            route = new List<Route>
                             {
                                 new Route
                                 {
                                      deliveryPoint = (int)DeliveryPointType.DistributionCenter,
                                       deliveries = deliveries
                                 }
                             }
                        }),
                        Encoding.UTF8, "application/json"));

                var contents = await response.Content.ReadAsStringAsync();
                var responseModels = JsonConvert.DeserializeObject<ShipmentResponseModel>(contents);

                foreach (var route in responseModels.route)
                {
                    foreach (var delivery in route.deliveries)
                    {
                        if (delivery.barcode.Contains("P8988000120") && delivery.State == (int)ShipmentStatus.Unloaded)
                        {
                            Assert.True(true);
                        }
                        if (delivery.barcode.Contains("C725799") && delivery.State == (int)ShipmentStatus.Unloaded)
                        {
                            Assert.True(true);
                        }
                    }
                }

                Assert.True(responseModels.route?.Where(x => x.deliveryPoint == (int)DeliveryPointType.DistributionCenter)?.FirstOrDefault()?.deliveries.Count(x => x.State == (int)ShipmentStatus.Unloaded) == 2);

                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Shipment_Transfer_Center_Delivery_Point_Should_Return_HttpStatusCode_Ok()
        {

            var deliveries = new List<Delivery>
                                       {
                                            new Delivery
                                           {
                                                barcode = "P9988000126",
                                           },
                                            new Delivery
                                           {
                                                barcode = "C725800",
                                           },
                                               new Delivery
                                           {
                                                barcode = "C725799",
                                           }
                                       };

            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("api/shipment"
                        , new StringContent(
                        JsonConvert.SerializeObject(new ShipmentRequestModel()
                        {
                            plate = "34 TL 34",
                            route = new List<Route>
                             {
                                 new Route
                                 {
                                      deliveryPoint = (int)DeliveryPointType.TransferCenter,
                                       deliveries = deliveries
                                 }
                             }
                        }),
                        Encoding.UTF8, "application/json"));

                var contents = await response.Content.ReadAsStringAsync();
                var responseModels = JsonConvert.DeserializeObject<ShipmentResponseModel>(contents);

                foreach (var route in responseModels.route)
                {
                    foreach (var delivery in route.deliveries)
                    {
                        if (delivery.barcode.Contains("P9988000126") && delivery.State == (int)ShipmentStatus.Unloaded)
                        {
                            Assert.True(true);
                        }
                        if (delivery.barcode.Contains("C725800") && delivery.State == (int)ShipmentStatus.Unloaded)
                        {
                            Assert.True(true);
                        }
                        if (delivery.barcode.Contains("C725799") && delivery.State == (int)ShipmentStatus.Loaded)
                        {
                            Assert.True(true);
                        }
                    }
                }

                Assert.True(responseModels.route?.Where(x => x.deliveryPoint == (int)DeliveryPointType.TransferCenter)?.FirstOrDefault()?.deliveries.Count(x => x.State == (int)ShipmentStatus.Unloaded) == 1);
                Assert.True(responseModels.route?.Where(x => x.deliveryPoint == (int)DeliveryPointType.TransferCenter)?.FirstOrDefault()?.deliveries.Count(x => x.State == (int)ShipmentStatus.Loaded) == 2);

                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
