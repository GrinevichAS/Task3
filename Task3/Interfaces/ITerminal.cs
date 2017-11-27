using System;
using Task3.EventsArgs;

namespace Task3.Interfaces
{
    public interface ITerminal
    {
        int Id { get; }
        IPort Port { get; }

        event EventHandler<CallEventArgs> CallEvent;
        event EventHandler<EndCallEventArgs> EndCallEvent;
        
        void HandleAnswerEvent(object o, CallEventArgs e);
        void HandleMessageFromAteEvent(object o, MessageFromAteEventArgs e);
        void ChangeTariff(ITariff tariff);
        void Call(string number);
        void EndCall();
        string GetNumber();
        void ConnectToPort(IPort port);
        void DissconnectFromPort();
    }
}