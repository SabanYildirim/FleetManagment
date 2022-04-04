using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Common
{
    public class DataNotFoundException : FleetManagementBaseException
    {
        public DataNotFoundException(int errorCode,string message) : base(errorCode,message)
        {
        }
    }
}
