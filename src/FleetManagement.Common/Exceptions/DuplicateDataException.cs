using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Common
{
    public class DuplicateDataException : FleetManagementBaseException
    {
        public DuplicateDataException(int errorCode, string message) : base(errorCode, message)
        {
        }
    }
}
