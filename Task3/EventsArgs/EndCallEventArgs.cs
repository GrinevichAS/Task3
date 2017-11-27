using System;
using Task3.Interfaces;

namespace Task3.EventsArgs
{
    public class EndCallEventArgs : EventArgs
    {
        public IPort InitiatorOfEnd { get; private set; }
        public DateTime TimeOfEndCall { get; private set; }
        public ICallData EndedCall { get; private set; }

        public EndCallEventArgs(IPort port)
        {
            InitiatorOfEnd = port;
            TimeOfEndCall = DateTime.Now;
        }

        public void SetEndedCall(ICallData call)
        {
            EndedCall = call;
        }
    }
}