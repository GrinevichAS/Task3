using System.Collections.Generic;

namespace Task3.Interfaces
{
    public interface IBillingSystem
    {
        List<IContract> Contracts { get; }
        List<ICallData> FinishedCalls { get; }
        
    }
}