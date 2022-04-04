using FleetManagement.Application.DTO.response;
using FleetManagement.Application.Interfaces;
using FleetManagement.Common;
using FleetManagement.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.Api.Controllers
{
    [Route("api/licenseplate")]
    [ApiController]
    public class LicensePlateController : ControllerBase
    {
        private readonly ILicensePlateService _licensePlateService;
        public LicensePlateController(ILicensePlateService licensePlateService)
        {
            _licensePlateService = licensePlateService;
        }

        [HttpPost]
        public SuccessResponseModel Add(string licensePlate)
        {
            ValidateRequest(licensePlate);

            _licensePlateService.AddPlate(licensePlate);

            return new SuccessResponseModel { Success = true };
        }

        private void ValidateRequest(string licensePlate)
        {
            if (string.IsNullOrEmpty(licensePlate))
                throw new ValidationException(AppErrorCodeConstants.RequestIsNotValid, $"License plate is empty.");
        }
    }
}
