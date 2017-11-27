using System;
using Task3.Interfaces;

namespace Task3.ATESystem
{
    public class CallData : ICallData
    {
        public IPort Caller { get; private set; }
        public IPort Receiver { get; private set; }
        public DateTime TimeOfBeginCall { get; private set; }
        public DateTime TimeOfEndCall { get; private set; }

        public CallData(IPort caller, IPort receiver)
        {
            Caller = caller;
            Receiver = receiver;
            TimeOfBeginCall = DateTime.Now;
        }

        public TimeSpan GetDuretionOfCall()
        {
            TimeSpan duration = TimeOfEndCall.Subtract(TimeOfBeginCall);
            return duration;
        }

        public void SetTimeOfEnd(DateTime time)
        {
            TimeOfEndCall = time;
        }
       
    }
}