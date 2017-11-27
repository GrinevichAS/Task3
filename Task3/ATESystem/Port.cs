using System;
using Task3.Enums;
using Task3.EventsArgs;
using Task3.Interfaces;

namespace Task3.ATESystem
{
    public class Port : IPort
    {
        public string Number { get; private set; }
        public int Id { get; private set; }
        public StatusOfPort PortStatus { get; private set; }
        public StatusOfCall CallStatus { get; private set; }
        public StatusOfContract ContractStatus { get; private set; }

        public event EventHandler<CallEventArgs> AnswerEvent = delegate { };
        public event EventHandler<CallEventArgs> Calling = delegate { };
        public event EventHandler<EndCallEventArgs> EndingCall = delegate { };
        public event EventHandler<MessageFromAteEventArgs> MessageFromATE = delegate { };
        public event EventHandler<ChangeTariffEventArgs> ChangingTariff = delegate { };
        public event EventHandler<GetHistoryEventArgs> GettingHistory = delegate { };

        public Port(int id, string number)
        {
            Number = number;
            Id = id;
            PortStatus = StatusOfPort.NotConnected;
            ContractStatus = StatusOfContract.NotContracted;
        }
        
        protected virtual void OnEndingCall(EndCallEventArgs e)
        {
            EndingCall.Invoke(this, e);
        }

        protected virtual void OnAnswerEvent(CallEventArgs e)
        {
            AnswerEvent.Invoke(this, e);
        }

        protected virtual void OnCalling(CallEventArgs e)
        {
            Calling.Invoke(this, e);
        }

        protected virtual void OnGettingHistory(GetHistoryEventArgs e)
        {
            GettingHistory.Invoke(this, e);
        }
        
        protected virtual void OnChangingTariff(ChangeTariffEventArgs e)
        {
            ChangingTariff.Invoke(this, e);
        }

        protected virtual void OnMessageFromATE(MessageFromAteEventArgs e)
        {
            MessageFromATE.Invoke(this, e);
        }

        public void HandleCallEvent(object o, CallEventArgs e)
        {
            OnCalling(e);
        }

        public void HandleEndCallEvent(object o, EndCallEventArgs e)
        {
            OnEndingCall(e);
        }

        public void HandleGetHistoryEvent(object o, GetHistoryEventArgs e)
        {
            OnGettingHistory(e);
        }

        public void HandleChangeTariffEvent(object o, ChangeTariffEventArgs e)
        {
            OnChangingTariff(e);
        }

        public void GetAnswer(CallEventArgs e)
        {
            OnAnswerEvent(e);
        }

        public void ATEMessageShow(MessageFromAteEventArgs e)
        {
            OnMessageFromATE(e);
        }

        public void ChangeCallStatus(StatusOfCall status)
        {
            CallStatus = status;
        }

        public void ChangeStatusOfPort()
        {
            PortStatus = PortStatus != StatusOfPort.NotConnected ? StatusOfPort.NotConnected : StatusOfPort.Connected;
        }

        public void ChangeStatusOfContract()
        {
            ContractStatus = ContractStatus != StatusOfContract.NotContracted ? StatusOfContract.NotContracted : StatusOfContract.Contracted;
        }
        
    }
}