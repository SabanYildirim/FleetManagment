﻿using FleetManagement.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Application.Interfaces
{
    public interface IPackageInfoService
    {
        void AddInfo(int shiptmentId, int weight);
    }
}