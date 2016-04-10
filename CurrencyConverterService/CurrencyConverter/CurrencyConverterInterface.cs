//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.ServiceModel.Web;
//using System.Text;

using System.ServiceModel;

namespace CurrencyConverterService
{
    [ServiceContract]
    public interface CurrencyConverterInterface
    {
        [OperationContract]
        string GetUsd(double curr);

        [OperationContract]
        string GetYen(double curr);

        [OperationContract]
        double GetCurrency(string curr);

        //[OperationContract]
        //CompositeType GetDataUsingDataContract(CompositeType composite);

        //[OperationContract]
        //CompositeType GetCurrGet(CompositeType exec);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    //}
}
