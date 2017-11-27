using System;
using Task3.Interfaces;

namespace Task3.EventsArgs
{
    public class ChangeTariffEventArgs : EventArgs
    {
        public IPort Port { get; private set; }
        public ITariff Tariff { get; private set; }

        public ChangeTariffEventArgs(IPort port, ITariff tariff)
        {
            Port = port;
            Tariff = tariff;
        }

    }
}