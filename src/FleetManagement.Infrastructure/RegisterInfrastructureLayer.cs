using FleetManagement.Infrastructure.Entities;
using FleetManagement.Infrastructure.Interfaces;
using FleetManagement.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManagement.Infrastructure
{
    public static class RegisterInfrastructureLayer
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddScoped<ILicensePlateRepository, LicensePlateRepository>();
            services.AddScoped<IDeliveryPointRepository, DeliveryPointRepository>();
            services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            services.AddScoped<IShipmentRepository, ShipmentRepository>();
            services.AddScoped<IPackageInfoRepository, VolumetricWeightRepository>();
        }
    }
}