using FleetManagement.Infrastructure.Entities;
using System.Collections.Generic;

namespace FleetManagement.Infrastructure.Interfaces
{
    public interface IShipmentRepository : IDataRepository<ShipmentEntity>
    {
        void UpdateStatus(List<string> barcode, int status);
        ShipmentEntity GetByBarcodeAndDeliveryPointType(string barcode, int deliveryPointType);
        bool HasShipment(string barcode);
        ShipmentEntity GetByBarcode(string barcode);
        IEnumerable<ShipmentEntity> GetShipments(List<string> barcodes);
    }
}
