using FleetManagement.Application.DTO;
using FleetManagement.Application.Enums;

namespace FleetManagement.Application.Interfaces
{
    public interface IPackagesService
    {
        int AddPackage(PackageServiceModel entity);
    }
}
