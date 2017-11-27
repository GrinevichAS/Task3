using System;
using Task3.Enums;
using Task3.EventsArgs;
using Task3.Interfaces;

namespace Task3.ATESystem
{
    public class Terminal : ITerminal
    {
        public int Id { get; private set; }
        public IPort Port { get; private set; }

        public event EventHandler<CallEventArgs> CallEvent = delegate { };
        public event EventHandler<EndCallEventArgs> EndCallEvent = delegate { };
        public event EventHandler<ChangeTariffEventArgs> ChangeTariffEvent = delegate { };
        public event EventHandler<GetHistoryEventArgs> GetHistoryEvent = delegate { };

        public Terminal(int id)
        {
            Id = id;
        }

        protected virtual void OnCall(CallEventArgs e)
        {
            CallEvent.Invoke(this, e);
        }

        public void Call(string number)
        {
            if (Port != null) OnCall(new CallEventArgs(Port, number));
        }

        protected virtual void OnEndCall(EndCallEventArgs e)
        {
            EndCallEvent.Invoke(this, e);
        }

        public void EndCall()
        {
            if (Port.CallStatus == StatusOfCall.Busy)
            {
                OnEndCall(new EndCallEventArgs(Port));
            }
        }

        public void HandleAnswerEvent(object o, CallEventArgs e)
        {
            Console.WriteLine("Call from {0}", e.PortOfCaller.Number);
            Console.WriteLine("Y - to answer");
            string answer = Console.ReadLine();
            e.SetAnswerStatus(answer);
        }

        protected virtual void OnGetHistoryEvent(GetHistoryEventArgs e)
        {
            GetHistoryEvent.Invoke(this, e);
        }

        protected virtual void OnChangeTariffEvent(ChangeTariffEventArgs e)
        {
            ChangeTariffEvent.Invoke(this, e);
        }

        public void HandleMessageFromAteEvent(object o, MessageFromAteEventArgs e)
        {
            if (e.MessageList != null)
            {
                foreach (var item in e.MessageList)
                {
                    Console.WriteLine(item);
                }
            }
            else Console.WriteLine(e.Message);
        }

        public void GetHistory()
        {
            OnGetHistoryEvent(new GetHistoryEventArgs(this.Id));
        }

        public void ChangeTariff(ITariff tariffPlan)
        {
            OnChangeTariffEvent(new ChangeTariffEventArgs(Port, tariffPlan));
        }
        
        public void ConnectToPort(IPort port)
        {
            if (Port == null)
            {
                Port = port;
                Port.ChangeCallStatus(StatusOfCall.Avaliable);
                Port.ChangeStatusOfPort();
                Port.AnswerEvent += HandleAnswerEvent;
                CallEvent += Port.HandleCallEvent;
                EndCallEvent += Port.HandleEndCallEvent;
                ChangeTariffEvent += Port.HandleChangeTariffEvent;
                Port.MessageFromATE += HandleMessageFromAteEvent;
                GetHistoryEvent += Port.HandleGetHistoryEvent;
            }
            else Console.WriteLine("Terminal {0} already has a port", Id);
        }

        public void DissconnectFromPort()
        {
            if (Port != null)
            {
                Port.ChangeCallStatus(StatusOfCall.NotAvalibale);
                Port.ChangeStatusOfPort();
                CallEvent -= Port.HandleCallEvent;
                Port.AnswerEvent -= HandleAnswerEvent;
                EndCallEvent -= Port.HandleEndCallEvent;
                ChangeTariffEvent -= Port.HandleChangeTariffEvent;
                Port.MessageFromATE += HandleMessageFromAteEvent;
                GetHistoryEvent -= Port.HandleGetHistoryEvent;
                Port = null;
            }
            else Console.WriteLine("Terminal {0} already disconected", Id);
        }

        public string GetNumber()
        {
            return Port == null ? null : Port.Number;
        }
        
    }
}