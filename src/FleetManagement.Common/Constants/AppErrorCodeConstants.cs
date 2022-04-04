using System;
using System.Collections.Generic;
using System.Text;

namespace FleetManagement.Common
{
    public class AppErrorCodeConstants
    {
        public const int BagNotFoundErrorCode = 2000;
        public const int BagFoundErrorCode = 2001;

        public const int PackageNotFoundErrorCode = 2002;
        public const int PackageFoundErrorCode = 2003;

        public const int PlateNotFoundErrorCode = 2004;
        public const int PlateFoundErrorCode = 2005;

        public const int DeliveryPointNotFoundErrorCode = 2006;
        public const int DeliveryPointFoundErrorCode = 2007;

        public const int ShipmentNotFoundCode = 2008;
        public const int ShipmentRouteErrorCode = 2009;

        public const int AssigmentFoundErrorCode = 20010;

        public const int BagAssigmentFoundErrorCode = 20011;
        public const int PackageAssigmentFoundErrorCode = 2012;


        public const int AddBagBarcodeWrongRoute = 2013;
        public const int RequestIsNotValid = 2014;

        public const int UnknownErrorCode = 5000;
    }
}
