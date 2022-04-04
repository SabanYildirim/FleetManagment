using FleetManagement.Application.DTO;
using FleetManagement.Application.DTO.request;
using FleetManagement.Application.DTO.response;
using FleetManagement.Application.Interfaces;
using FleetManagement.Common;
using FleetManagement.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.Api.Controllers
{
    [Route("api/package")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        public readonly IPackagesService _packagesService;
        public PackageController(IPackagesService packagesService)
        {
            _packagesService = packagesService;
        }

        [HttpPost]
        public SuccessResponseModel Add([FromBody] PackagesRequestModel requestModel)
        {
            ValidateRequest(requestModel);

            PackageServiceModel packageServiceModel = new PackageServiceModel();
            packageServiceModel.Barcode = requestModel.Barcode;
            packageServiceModel.DeliveryPointforUnloading = requestModel.DeliveryPointforUnloading;
            packageServiceModel.VolumetricWeight = requestModel.VolumetricWeight;

            _packagesService.AddPackage(packageServiceModel);

            return new SuccessResponseModel { Success = true };
        }

        private void ValidateRequest(PackagesRequestModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.Barcode))
                throw new ValidationException(AppErrorCodeConstants.RequestIsNotValid, $"Package Barcode is empty.");
            if (requestModel.DeliveryPointforUnloading == 0)
                throw new ValidationException(AppErrorCodeConstants.RequestIsNotValid, $"Delivery Point for Unloading is empty.");
            if (requestModel.VolumetricWeight <= 0)
                throw new ValidationException(AppErrorCodeConstants.RequestIsNotValid, $"Volumetric Weight can not be less then 0 or empty.");
        }
    }
}
