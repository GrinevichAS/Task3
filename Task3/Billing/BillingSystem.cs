using System.Collections.Generic;
using System.Linq;
using Task3.EventsArgs;
using Task3.Interfaces;

namespace Task3.Billing
{
    public class BillingSystem : IBillingSystem
    {
        public List<IContract> Contracts { get; private set; }
        public List<ICallData> FinishedCalls { get; private set; }

        private List<string> FindHistory(int id)
        {
            var calls = from item in FinishedCalls
                          where item.Caller.Id == id || item.Receiver.Id == id
                          select item;
            return calls.Select(item => item.Caller.Number + " called  " + item.Receiver.Number + " and talking for  " + item.GetDuretionOfCall().Seconds).ToList();
        }

        public IContract FindContract(int id)
        {
            return Contracts.FirstOrDefault(item => item.IdPort == id);
        }

        public void HandleGetHistoryEvent(object o, GetHistoryEventArgs e)
        {
            e.SetHistory(FindHistory(e.IdPort));
        }

        public BillingSystem()
        {
            Contracts = new List<IContract>();
            FinishedCalls = new List<ICallData>();
        }
    }
}
