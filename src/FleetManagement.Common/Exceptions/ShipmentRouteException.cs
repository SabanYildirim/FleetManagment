using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Common.Exceptions
{
    public class ShipmentRouteException : FleetManagementBaseException
    {
        public ShipmentRouteException(int errorCode, string message) : base(errorCode, message)
        {
        }
    }
}
