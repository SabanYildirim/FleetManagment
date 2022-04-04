﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Application.Interfaces
{
    public interface IShipmentValidatorService
    {
        IDeliveryPointValidator getValidator(int deliveryPoint);
    }
}
