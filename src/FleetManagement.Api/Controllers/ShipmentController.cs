using FleetManagement.Application.DTO.request;
using FleetManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FleetManagement.Common;
using FleetManagement.Application.Enums;
using FleetManagement.Application.DTO.response;
using System.Collections.Generic;
using FleetManagement.Common.Exceptions;

namespace FleetManagement.Api.Controllers
{
    [Route("api/shipment")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentValidatorService _shipmentValidatorService;
        private readonly ILicensePlateService _licensePlateService;
        private readonly IShipmentManager _shipmentManager;
        private readonly IShipmentService _shipmentService;

        public ShipmentController(
            IShipmentValidatorService shipmentValidatorService,
            ILicensePlateService licensePlateService,
            IShipmentManager shipmentManager,
            IShipmentService shipmentService)
        {
            _shipmentValidatorService = shipmentValidatorService;
            _licensePlateService = licensePlateService;
            _shipmentManager = shipmentManager;
            _shipmentService = shipmentService;
        }

        [HttpPost]
        public ShipmentResponseModel shipment([FromBody] ShipmentRequestModel requestModel)
        {
            ValdiateRequest(requestModel);

            ShipmentResponseModel result = new ShipmentResponseModel();
            result.plate = requestModel.plate;

            foreach (var route in requestModel.route)
            {
                var barcodes = route.deliveries.Select(x => x.barcode).ToList();
                List<string> existBarcodes = _shipmentService.GetShipmentBarcodes(barcodes);

                _shipmentService.UpdateStatus(existBarcodes, ShipmentStatus.Loaded);

                var validatorService = _shipmentValidatorService.getValidator(route.deliveryPoint);
                var validBarcodes = validatorService.getValidShipments(existBarcodes);

                List<DeliveryResponseModel> deliveries = new List<DeliveryResponseModel>();

                foreach (var barcode in existBarcodes)
                {
                    if (validBarcodes.Contains(barcode))
                    {
                        _shipmentManager.ProcessShipment(barcode);
                        deliveries.Add(new DeliveryResponseModel { barcode = barcode, State = (int)ShipmentStatus.Unloaded });
                    }
                    else
                    {
                        deliveries.Add(new DeliveryResponseModel { barcode = barcode, State = (int)ShipmentStatus.Loaded });
                    }
                }

                result.route.Add(new RouteResponseModel
                {
                    deliveryPoint = route.deliveryPoint,
                    deliveries = deliveries
                });
            }

            return result;
        }

        private void ValdiateRequest(ShipmentRequestModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.plate))
            {
                throw new ValidationException(AppErrorCodeConstants.RequestIsNotValid, $"Plate is empty.");
            }
            if (requestModel.route == null || !requestModel.route.Any())
            {
                throw new ValidationException(AppErrorCodeConstants.RequestIsNotValid, $"Route is empty.");
            }
            if (requestModel.route != null && requestModel.route.FirstOrDefault().deliveryPoint == 0)
            {
                throw new ValidationException(AppErrorCodeConstants.RequestIsNotValid, $"Delivery Point is empty.");
            }
            if (requestModel.route != null && (requestModel.route.FirstOrDefault().deliveries == null || !requestModel.route.FirstOrDefault().deliveries.Any()))
            {
                throw new ValidationException(AppErrorCodeConstants.RequestIsNotValid, $"Deliveries is empty.");
            }
            if (!_licensePlateService.HasPlate(requestModel.plate))
            {
                throw new DataNotFoundException(AppErrorCodeConstants.PlateNotFoundErrorCode, $"Plate not found ({requestModel.plate})");
            }
        }
    }
}

