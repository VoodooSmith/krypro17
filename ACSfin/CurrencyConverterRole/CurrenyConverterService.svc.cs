using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CurrencyConverterService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class CurrencyConverter : CurrencyConverterInterface
    {
        public string GetUsd(double curr)
        {
            /* Convert EUR to USD */
            double foreignex = 0;
            double rate = 1.1363;

            foreignex = curr * rate;
            return foreignex.ToString();

        }

        public string GetYen(double curr)
        {
            /* Convert EUR to YEN */
            double foreignex = 0;
            double rate = 123.36;

            foreignex = curr * rate;
            return foreignex.ToString();
        }
    }
}
