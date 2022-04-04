using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Infrastructure.Interfaces
{
    public interface ILicensePlateRepository : IDataRepository<LicensePlateEntity>
    {
        bool HasPlate(string plate);
    }
}
