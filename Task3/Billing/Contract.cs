using System;
using Task3.EventsArgs;
using Task3.Interfaces;

namespace Task3.Billing
{
    public class Contract : IContract
    {
        public int IdPort { get; private set; }
        public ITariff Tariff { get; private set; }
        public double Payment { get; set; }

        public Contract(int id, ITariff tariff)
        {
            IdPort = id;
            Tariff = tariff;
        }

        public void HandleChangeTariffEvent(object o, ChangeTariffEventArgs e)
        {
            Tariff = e.Tariff; 
        }

        public void HandleCostOfCall(object o, EndCallEventArgs e)
        {
            Payment += e.EndedCall.GetDuretionOfCall().TotalSeconds * Tariff.Cost;
        }
    }
}