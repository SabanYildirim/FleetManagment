using FleetManagement.Application.DTO.request;
using FleetManagement.Application.DTO.response;
using FleetManagement.Application.Interfaces;
using FleetManagement.Common;
using FleetManagement.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.Api.Controllers
{
    [Route("api/assignment")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        readonly IAssignmentService _packagestoBagsService;
        public AssignmentController(IAssignmentService packagestoBagsService)
        {
            _packagestoBagsService = packagestoBagsService;
        }

        [HttpPost]
        public SuccessResponseModel Add([FromBody] AssignmentRequetModel requestModel)
        {
            ValidateRequest(requestModel);

            _packagestoBagsService.Assign(requestModel.PackageBarcode, requestModel.BagBarcode);

            return new SuccessResponseModel { Success = true };
        }

        private void ValidateRequest(AssignmentRequetModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.BagBarcode))
                throw new ValidationException(AppErrorCodeConstants.RequestIsNotValid, $"Bag Barcode is empty.");
            if (string.IsNullOrEmpty(requestModel.PackageBarcode))
                throw new ValidationException(AppErrorCodeConstants.RequestIsNotValid, $"Package Barcode is empty.");
        }
    }
}
