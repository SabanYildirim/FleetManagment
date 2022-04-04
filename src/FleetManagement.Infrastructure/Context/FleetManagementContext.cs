using FleetManagement.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Infrastructure.Context
{
    public class FleetManagementContext : DbContext
    {
        public FleetManagementContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<AssignmentEntity> Assignments { get; set; }
        public DbSet<DeliveryPointEntity> DeliveryPointies { get; set; }
        public DbSet<LicensePlateEntity> LicensePlates { get; set; }
        public DbSet<ShipmentEntity> Shipments { get; set; }
        public DbSet<PackageInfo> VolumetricWeights { get; set; }
    }
}
