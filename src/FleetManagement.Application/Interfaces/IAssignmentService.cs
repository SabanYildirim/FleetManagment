using FleetManagement.Application.DTO;
using FleetManagement.Application.DTO.request;
using System.Collections.Generic;

namespace FleetManagement.Application.Interfaces
{
    public interface IAssignmentService
    {
        void Assign(string packageBarcode, string bagBarcode);
        IEnumerable<AssignmentSeviceModel> GetByBagBarcodes(string barcode);
        IEnumerable<AssignmentSeviceModel> GetByPackageBarcodes(string barcode);
        bool HasAssigment(string barcode);
    }
}
