namespace FleetManagement.Application.DTO.request
{
    public class PackagesRequestModel
    {
        public string Barcode { get; set; }
        public int DeliveryPointforUnloading { get; set; }
        public int VolumetricWeight { get; set; }
    }
}
