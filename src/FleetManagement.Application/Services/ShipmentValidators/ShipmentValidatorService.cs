using FleetManagement.Application.Interfaces;

namespace FleetManagement.Application.Services
{
    public class ShipmentValidatorService : IShipmentValidatorService
    {
        private readonly IDeliveryPointService _deliveryPointService;
        private readonly IDeliveryPointValidator _branchDeliveryPointValidator;
        private readonly IDeliveryPointValidator _distributionCenterDeliveryPointValidator;
        private readonly IDeliveryPointValidator _transferCenterDeliveryPointValidator;

        public ShipmentValidatorService(IDeliveryPointService deliveryPointService,
            IBranchDeliveryPointValidator branchDeliveryPointValidator,
            IDistributionCenterPointValidator distributionCenterDeliveryPointValidator,
            ITransferCenterDeliveryPointValidator transferCenterDeliveryPointValidator)
        {
            _deliveryPointService = deliveryPointService;
            _branchDeliveryPointValidator = branchDeliveryPointValidator;
            _transferCenterDeliveryPointValidator = transferCenterDeliveryPointValidator;
            _distributionCenterDeliveryPointValidator = distributionCenterDeliveryPointValidator;
        }

        public IDeliveryPointValidator getValidator(int deliveryPoint)
        {
            var deliveryPointType = _deliveryPointService.GetDeliveryPointType(deliveryPoint);

            switch (deliveryPointType)
            {
                case Enums.DeliveryPointType.Branch:
                    return _branchDeliveryPointValidator;
                case Enums.DeliveryPointType.DistributionCenter:
                    return _distributionCenterDeliveryPointValidator;
                case Enums.DeliveryPointType.TransferCenter:
                    return _transferCenterDeliveryPointValidator;
                default:
                    break;
            }
            return null;
        }
    }
}
