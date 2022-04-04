using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Application.Interfaces
{
    public interface IShipmentManager
    {
        void ProcessShipment(string barcode);
    }
}
