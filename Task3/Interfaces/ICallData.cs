using System;

namespace Task3.Interfaces
{
    public interface ICallData
    {
        IPort Caller { get; }
        IPort Receiver { get; }
        DateTime TimeOfBeginCall { get; }
        DateTime TimeOfEndCall { get; }

        void SetTimeOfEnd(DateTime time);
        TimeSpan GetDuretionOfCall();
    }
}