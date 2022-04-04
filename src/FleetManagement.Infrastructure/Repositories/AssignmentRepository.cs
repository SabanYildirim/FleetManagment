using FleetManagement.Infrastructure.Context;
using FleetManagement.Infrastructure.Entities;
using FleetManagement.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FleetManagement.Infrastructure.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly FleetManagementContext _fleetManagementContext;

        public AssignmentRepository(FleetManagementContext fleetManagementContext)
        {
            _fleetManagementContext = fleetManagementContext;
        }

        public int Add(AssignmentEntity entity)
        {
            _fleetManagementContext.Assignments.Add(entity);
            _fleetManagementContext.SaveChanges();
            return entity.Id;
        }

        public void Delete(AssignmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssignmentEntity> GetByBagBarcodes(string barcode)
        {
            return _fleetManagementContext.Assignments.Where(x => x.BagBarcode == barcode);
        }

        public IEnumerable<AssignmentEntity> GetByPackageBarcodes(string barcode)
        {
            return _fleetManagementContext.Assignments.Where(x => x.PackageBarcode == barcode);
        }

        public IEnumerable<AssignmentEntity> GetAll()
        {
            return _fleetManagementContext.Assignments.ToList();
        }

        public void Update(AssignmentEntity dbEntity, AssignmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public AssignmentEntity Get(long id)
        {
            return _fleetManagementContext.Assignments.FirstOrDefault(x => x.Id == id);
        }

        public bool HasAssignment(string barcode)
        {
            return _fleetManagementContext.Assignments.Any(x => x.PackageBarcode == barcode);
        }

        public bool HasLinkedPackageToBag(string packageBarcode, string bagBarcode)
        {
            return _fleetManagementContext.Assignments.Any(x => x.PackageBarcode == packageBarcode && x.BagBarcode == bagBarcode);
        }
    }
}
