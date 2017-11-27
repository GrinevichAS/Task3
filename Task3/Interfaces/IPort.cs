using System;
using Task3.Enums;
using Task3.EventsArgs;

namespace Task3.Interfaces
{
    public interface IPort
    {
        int Id { get; }
        string Number { get; }
        StatusOfContract ContractStatus { get; }
        StatusOfPort PortStatus { get; }
        StatusOfCall CallStatus { get; }

        event EventHandler<CallEventArgs> Calling;
        event EventHandler<EndCallEventArgs> EndingCall;
        event EventHandler<CallEventArgs> AnswerEvent;
        event EventHandler<MessageFromAteEventArgs> MessageFromATE;
        event EventHandler<GetHistoryEventArgs> GettingHistory;
        event EventHandler<ChangeTariffEventArgs> ChangingTariff;
       
        void HandleChangeTariffEvent(object o, ChangeTariffEventArgs e);
        void HandleEndCallEvent(object o, EndCallEventArgs e);
        void HandleCallEvent(object o, CallEventArgs e);
        void ATEMessageShow(MessageFromAteEventArgs e);
        void GetAnswer(CallEventArgs e);
        void ChangeCallStatus(StatusOfCall status);
        void HandleGetHistoryEvent(object o, GetHistoryEventArgs e);
        void ChangeStatusOfPort();
        void ChangeStatusOfContract();
    }
}