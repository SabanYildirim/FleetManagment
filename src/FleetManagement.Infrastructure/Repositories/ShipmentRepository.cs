using FleetManagement.Infrastructure.Context;
using FleetManagement.Infrastructure.Entities;
using FleetManagement.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FleetManagement.Infrastructure.Repositories
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly FleetManagementContext _fleetManagementContext;
        public ShipmentRepository(FleetManagementContext fleetManagementContext)
        {
            _fleetManagementContext = fleetManagementContext;
        }

        public int Add(ShipmentEntity entity)
        {
            _fleetManagementContext.Shipments.Add(entity);
            _fleetManagementContext.SaveChanges();
            return entity.Id;
        }

        public void Delete(ShipmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public ShipmentEntity GetByBarcodeAndDeliveryPointType(string barcode, int deliveryPointType)
        {
            return _fleetManagementContext.Shipments.FirstOrDefault(x => x.Barcode == barcode && x.DeliveryPointForUnloading == deliveryPointType);
        }

        public IEnumerable<ShipmentEntity> GetAll()
        {
            return _fleetManagementContext.Shipments.ToList();
        }

        public void Update(ShipmentEntity dbEntity, ShipmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateStatus(List<string> barcodes, int status)
        {
            _fleetManagementContext.Shipments.Where(e => barcodes.Contains(e.Barcode)).ToList().ForEach(a => a.Status = status);
            _fleetManagementContext.SaveChanges();
        }

        public ShipmentEntity Get(long id)
        {
            throw new NotImplementedException();
        }

        public ShipmentEntity GetByBarcode(string barcode)
        {
            return _fleetManagementContext.Shipments.FirstOrDefault(x => x.Barcode == barcode);
        }

        public bool HasShipment(string barcode)
        {
            return _fleetManagementContext.Shipments.Any(x => x.Barcode == barcode);
        }

        public IEnumerable<ShipmentEntity> GetShipments(List<string> barcodes)
        {
            return _fleetManagementContext.Shipments.Where(e => barcodes.Contains(e.Barcode));
        }
    }
}
