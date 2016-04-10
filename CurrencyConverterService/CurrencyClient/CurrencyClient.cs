using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using CurrencyConverterService;


class CurrClient
{
    static void Main(string[] args)
    {
        string cash;
        Uri webAdress = new Uri("http://localhost:8080");
        double value;

        //WebServiceHost host = new WebServiceHost(typeof(CurrencyConverterInterface), webAdress);

        /* Hosting the Webservice */
        WebServiceHost host = new WebServiceHost(typeof(CurrencyConverter), webAdress);
        try
        {
            /* Adds a service endpoint to the hosted service with a specified contract, binding, and endpoint address. */
            ServiceEndpoint ep = host.AddServiceEndpoint(typeof(CurrencyConverterInterface), new WebHttpBinding(), "");
            //ServiceEndpoint ep = host.AddServiceEndpoint(typeof(CurrencyConverterInterface), new WebHttpBinding(), "");
            host.Open();
            using (ChannelFactory<CurrencyConverterInterface> cf = new ChannelFactory<CurrencyConverterInterface>(new WebHttpBinding(), "http://localhost:8080"))
            {
                cf.Endpoint.Behaviors.Add(new WebHttpBehavior());

                CurrencyConverterInterface channel = cf.CreateChannel();

                Console.WriteLine("");
                
                Console.WriteLine("Calling EchoWithPost via HTTP POST: ");
                cash = channel.GetUsd(1);
                Console.WriteLine("   USD: {0}", cash);
                cash = channel.GetYen(1);
                Console.WriteLine("   YEN: {0}", cash);
                Console.WriteLine("");

                value = channel.GetCurrency(cash);
            }

            Console.WriteLine("Press <ENTER> to terminate");
            Console.ReadLine();

            //host.Close();
        }
        catch (CommunicationException cex)
        {
            Console.WriteLine("An exception occurred: {0}", cex.Message);
            host.Abort();
        }
    }
}
 