using FleetManagement.Application.DTO;
using FleetManagement.Application.DTO.response;
using FleetManagement.Application.Interfaces;
using FleetManagement.Common;
using FleetManagement.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.Api.Controllers
{
    [Route("api/bag")]
    [ApiController]
    public class BagController : ControllerBase
    {
        private readonly IBagService _bagService;
        public BagController(IBagService bagService)
        {
            _bagService = bagService;
        }

        [HttpPost]
        public SuccessResponseModel Add([FromBody] BagRequestModel requestModel)
        {
            ValidateRequest(requestModel);

            BagServiceModel bagServiceModel = new BagServiceModel();
            bagServiceModel.Barcode = requestModel.Barcode;
            bagServiceModel.DeliveryPointforUnloading = requestModel.DeliveryPointforUnloading;

            _bagService.AddBag(bagServiceModel);

            return new SuccessResponseModel { Success = true };
        }

        private void ValidateRequest(BagRequestModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.Barcode))
                throw new ValidationException(AppErrorCodeConstants.RequestIsNotValid, $"Bag Barcode is empty.");
            if (requestModel.DeliveryPointforUnloading == 0)
                throw new ValidationException(AppErrorCodeConstants.RequestIsNotValid, $"Delivery Point for Unloading is empty.");
        }
    }
}
