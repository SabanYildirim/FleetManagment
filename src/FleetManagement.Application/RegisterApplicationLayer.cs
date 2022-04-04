using FleetManagement.Application.Interfaces;
using FleetManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManagement.Application
{
    public static class RegisterApplicationLayer
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddTransient<ILicensePlateService, LicensePlateService>();
            services.AddTransient<IDeliveryPointService, DeliveryPointService>();
            services.AddTransient<IBagService, BagService>();
            services.AddTransient<IPackagesService, PackageService>();
            services.AddTransient<IDistributionCenterPointValidator, DistributionCenterPointValidator>();
            services.AddTransient<ITransferCenterDeliveryPointValidator, TransferCenterDeliveryPointValidator>();
            services.AddTransient<IBranchDeliveryPointValidator, BranchDeliveryPointValidator>();
            services.AddTransient<IShipmentValidatorService, ShipmentValidatorService>();
            services.AddTransient<IShipmentManager, ShipmentManager>();
            services.AddTransient<IShipmentService, ShipmentService>();
            services.AddTransient<IPackageInfoService, PackageInfoService>();
            services.AddTransient<IAssignmentService, AssignmentService>();
        }
    }
}
