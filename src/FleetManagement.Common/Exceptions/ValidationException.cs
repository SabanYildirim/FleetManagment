using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Common.Exceptions
{
    public class ValidationException : FleetManagementBaseException
    {
        public ValidationException(int errorCode, string message) : base(errorCode, message)
        {
        }
    }
}
