using IFaxBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaxBusiness
{
    public class MyFaxBusiness:MarshalByRefObject
    {
        public static event FaxEventHandler FaxSendedEvent;

        #region

        public void SendFax(string fax)
        {
            if (FaxSendedEvent != null)
            {
                FaxSendedEvent(fax);
            }
        }

        #endregion

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}
