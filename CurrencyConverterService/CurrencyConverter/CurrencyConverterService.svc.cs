using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.ServiceModel;
//using System.ServiceModel.Web;
//using System.Text;
using System.Xml;

namespace CurrencyConverterService  
{
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

        public double GetCurrency(string curr)
        {
            double value = 0.0;

            XmlDocument doc = new XmlDocument();
            doc.Load("http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("gesmes:Envelope");
            Console.WriteLine(doc.DocumentElement);

            foreach (XmlNode node in nodes)
            {
                Console.WriteLine(node);
            }
            //XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            //nsmgr.AddNamespace("money", "schema");
            //XmlNodeList nodes = root.SelectNodes("Envelope/Cube");

            //foreach (XmlNode node in root)
            //{
            //    string currency = node["temp_f"].InnerText;
            //    string rate = node["temp_c"].InnerText;

            //    Console.WriteLine("Currency: {0}\nRate: {1}\n", currency, rate);

            //}
            return value;
        }

    //public CompositeType GetCurrGet(CompositeType exec)
    //{
    //    if(exec ==null)
    //    {
    //        throw new ArgumentNullException("composite");
    //    }
    //    if(exec.BoolValue)
    //    {
    //        exec.StringValue += "Suffix";
    //    }
    //    return exec;
    //}


    //public CompositeType GetDataUsingDataContract(CompositeType composite)
    //{
    //    if (composite == null)
    //    {
    //        throw new ArgumentNullException("composite");
    //    }
    //    if (composite.BoolValue)
    //    {
    //        composite.StringValue += "Suffix";
    //    }
    //    return composite;
    //}
}
}
