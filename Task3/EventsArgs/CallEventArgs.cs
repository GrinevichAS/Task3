using System;
using Task3.Enums;
using Task3.Interfaces;

namespace Task3.EventsArgs
{
    public class CallEventArgs : EventArgs
    {
        public IPort PortOfCaller { get; private set; }
        public string ReceiverNumber { get; private set; }
        public StatusOfAnswer AnswerStatus { get; private set; }

        public CallEventArgs(IPort port, string reciver)
        {
            PortOfCaller = port;
            ReceiverNumber = reciver;
        }

        public void SetAnswerStatus(string answer)
        {
            if (answer == "Y" || answer == "y")
            {
                AnswerStatus = StatusOfAnswer.Answer;
            }
            else AnswerStatus = StatusOfAnswer.Declined;
        }
    }
}