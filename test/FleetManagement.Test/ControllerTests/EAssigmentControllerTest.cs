using FleetManagement.Application.DTO.request;
using FleetManagement.Common.Exceptions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
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
    [Collection("C1"), Order(5)]
    public class EAssigmentControllerTest : IClassFixture<WebApplicationFactory<FleetManagement.Api.Startup>>
    {
        [Fact]
        public async Task Assigment_Distribution_Center_Barcodes_Should_Return_HttpStatusCode_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/assignment"
                        , new StringContent(
                        JsonConvert.SerializeObject(new AssignmentRequetModel()
                        {
                            BagBarcode = "P8988000120",
                            PackageBarcode = "C725799"
                        }),
                    Encoding.UTF8,
                    "application/json"));

                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Assigment_Transfer_Center_Barcodes_Should_Return_HttpStatusCode_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/assignment"
                        , new StringContent(
                        JsonConvert.SerializeObject(new AssignmentRequetModel()
                        {
                            BagBarcode = "P9988000126",
                            PackageBarcode = "C725800"
                        }),
                    Encoding.UTF8,
                    "application/json"));

                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Assigment_BagBarcode_Empty_Should_Return_ValidationException()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/assignment"
                        , new StringContent(
                        JsonConvert.SerializeObject(new AssignmentRequetModel()
                        {
                            BagBarcode = "",
                            PackageBarcode = "C725799"
                        }),
                    Encoding.UTF8,
                    "application/json"));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task Assigment_PackageBarcode_Empty_Should_Return_ValidationException()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/assignment"
                        , new StringContent(
                        JsonConvert.SerializeObject(new AssignmentRequetModel()
                        {
                            BagBarcode = "C725799",
                            PackageBarcode = ""
                        }),
                    Encoding.UTF8,
                    "application/json"));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }
    }
}
