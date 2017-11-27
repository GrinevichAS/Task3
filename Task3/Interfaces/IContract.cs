using System;
using Task3.EventsArgs;

namespace Task3.Interfaces
{
    public interface IContract
    {
        int IdPort { get; }
        ITariff Tariff { get; }
        
        void HandleChangeTariffEvent(object o, ChangeTariffEventArgs e);
        void HandleCostOfCall(object o, EndCallEventArgs e);
    }
}