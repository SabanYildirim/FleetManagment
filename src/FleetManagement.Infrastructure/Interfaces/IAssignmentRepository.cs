using FleetManagement.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Infrastructure.Interfaces
{
    public interface IAssignmentRepository : IDataRepository<AssignmentEntity>
    {
        public IEnumerable<AssignmentEntity> GetByBagBarcodes(string barcode);
        public IEnumerable<AssignmentEntity> GetByPackageBarcodes(string barcode);
        bool HasAssignment(string barcode);
        bool HasLinkedPackageToBag(string packageBarcode,string bagBarcode);
    }
}
