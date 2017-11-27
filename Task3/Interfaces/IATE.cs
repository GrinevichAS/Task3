using System.Collections.Generic;

namespace Task3.Interfaces
{
    public interface IATE
    {
        List<IPort> Ports { get; }

        void AddPort();
        IPort MakeContract(ITariff tariff, int id);
    }
}