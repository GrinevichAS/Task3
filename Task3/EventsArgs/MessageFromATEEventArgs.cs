using System;
using System.Collections.Generic;

namespace Task3.EventsArgs
{
    public class MessageFromAteEventArgs : EventArgs
    {
        public string Message { get; private set; }
        public List<String> MessageList { get; private set; }

        public MessageFromAteEventArgs(string message)
        {
            Message = message;
        }

        public MessageFromAteEventArgs(List<string> list)
        {
            MessageList = list;
        }

    }
}