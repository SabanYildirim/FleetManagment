using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Common
{
    public class FleetManagementBaseException : Exception
    {
        public int ErrorCode { get; private set; }
        public FleetManagementBaseException(int errorCode, string message) : base(message)
        {
            this.ErrorCode = errorCode;
        }
    }
}
