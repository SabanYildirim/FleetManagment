using FleetManagement.Application.DTO;
using FleetManagement.Application.DTO.response;
using FleetManagement.Application.Interfaces;
using FleetManagement.Common;
using FleetManagement.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManagement.Api.Controllers
{
    [Route("api/deliverypoint")]
    [ApiController]
    public class DeliveryPointController : ControllerBase
    {
        public readonly IDeliveryPointService _deliveryPointService;
        public DeliveryPointController(IDeliveryPointService deliveryPointService)
        {
            _deliveryPointService = deliveryPointService;
        }

        [HttpPost]
        public SuccessResponseModel Add([FromBody]DeliveryPointRequestModel requestModel)
        {
            ValidateRequest(requestModel);

            DeliveryPointServiceModel deliveryPointServiceModel = new DeliveryPointServiceModel();
            deliveryPointServiceModel.DeliveryPointName = requestModel.DeliveryPointName;
            deliveryPointServiceModel.Value = requestModel.Value;

            _deliveryPointService.AddDeliveryPoint(deliveryPointServiceModel);

            return new SuccessResponseModel { Success = true };
        }

        private void ValidateRequest(DeliveryPointRequestModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.DeliveryPointName))
                throw new ValidationException(AppErrorCodeConstants.RequestIsNotValid, $"Delivery Point Name is empty.");
            if (requestModel.Value == 0)
                throw new ValidationException(AppErrorCodeConstants.RequestIsNotValid, $"Delivery Point Value is empty.");
        }
    }
}
