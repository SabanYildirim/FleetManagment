using System.Threading.Tasks;

namespace FleetManagement.Application.Interfaces
{
    public interface ILicensePlateService
    {
        void AddPlate(string plate);
        bool HasPlate(string plate);
    }
}
