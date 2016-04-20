using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CurrencyConverterService
{
    [ServiceContract]
    public interface CurrencyConverterInterface
    {
        [OperationContract]
        string GetUsd(double curr);

        [OperationContract]
        string GetYen(double curr);
    }
}

