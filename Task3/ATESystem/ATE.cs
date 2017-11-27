using System;
using System.Collections.Generic;
using System.Linq;
using Task3.Billing;
using Task3.Enums;
using Task3.EventsArgs;
using Task3.Interfaces;

namespace Task3.ATESystem
{
    public class ATE : IATE
    {
        public List<IPort> Ports { get; private set; }
        public BillingSystem Abonents { get; private set; }
        private List<ICallData> OnGoingCalls { get; set; }

        public ATE()
        {
            Ports = new List<IPort>();
            Abonents = new BillingSystem();
            OnGoingCalls = new List<ICallData>();
        }

        public void AddPort()
        {
            Ports.Add(new Port(Ports.Count + 1, GenerateNumber()));
            Ports.Last().Calling += HandleCallEvent;
            Ports.Last().EndingCall += HandleEndCallEvent;
        }

        public IPort MakeContract(ITariff tariff, int id)
        {
            var item = Ports.Find(x => x.Id == id);
            if (item == null)
            {
                AddPort();
                Abonents.Contracts.Add(new Contract(Ports.Last().Id, tariff));
                Ports.Last().ChangeStatusOfContract();
                Ports.Last().EndingCall += Abonents.FindContract(Ports.Last().Id).HandleCostOfCall;
                Ports.Last().GettingHistory += Abonents.HandleGetHistoryEvent;
                Ports.Last().GettingHistory += HandleGetHistoryEvent;
                Ports.Last().ChangingTariff += Abonents.FindContract(Ports.Last().Id).HandleChangeTariffEvent;
                return Ports.Last();
            }
            else
            {
                IPort fail = new Port(-1, "");
                return fail;
            }


        }

        public void HandleGetHistoryEvent(object o, GetHistoryEventArgs e)
        {
            var item = Ports.Find(x => x.Id == e.IdPort);
            item.ATEMessageShow(new MessageFromAteEventArgs(e.History));
        }

        private bool CheckNumber(string number)
        {
            var finding = from port in Ports
                          select port.Number;
            return finding.All(item => item != number);
        }

        private bool IsNumberExist(string number)
        {
            return Ports.Any(item => item.Number == number);
        }

        private string GenerateNumber()
        {
            string number;
            Random rnd = new Random();
            do
            {
                number = rnd.Next(1000000, 9999999).ToString();
            }
            while (!CheckNumber(number));
            return number;
        }

        public void HandleEndCallEvent(object o, EndCallEventArgs e)
        {
            var item = OnGoingCalls.Find(x => e.InitiatorOfEnd == x.Caller || e.InitiatorOfEnd == x.Receiver);
            if (item != null)
            {
                item.SetTimeOfEnd(e.TimeOfEndCall);
                Abonents.FinishedCalls.Add(item);
                e.SetEndedCall(item);
                OnGoingCalls.Remove(item);
                item.Caller.ChangeCallStatus(StatusOfCall.Avaliable);
                item.Receiver.ChangeCallStatus(StatusOfCall.Avaliable);
            }
        }

        public void HandleCallEvent(object o, CallEventArgs e)
        {
            if (IsNumberExist(e.ReceiverNumber) && e.PortOfCaller.Number != e.ReceiverNumber)
            {
                var item = Ports.Find(x => x.PortStatus == StatusOfPort.Connected && x.CallStatus == StatusOfCall.Avaliable && e.ReceiverNumber == x.Number);
                if (item != null)
                {
                    item.GetAnswer(e);
                    if (e.AnswerStatus == StatusOfAnswer.Answer)
                    {
                        Console.WriteLine("Start of call");
                        item.ChangeCallStatus(StatusOfCall.Busy);
                        e.PortOfCaller.ChangeCallStatus(StatusOfCall.Busy);
                        OnGoingCalls.Add(new CallData(e.PortOfCaller, item));
                    }
                    else e.PortOfCaller.ATEMessageShow(new MessageFromAteEventArgs("Desclined"));
                }
                else e.PortOfCaller.ATEMessageShow(new MessageFromAteEventArgs("Receiver is busy"));
            }
            else e.PortOfCaller.ATEMessageShow(new MessageFromAteEventArgs("Number is wrong"));
        }
        
    }
}