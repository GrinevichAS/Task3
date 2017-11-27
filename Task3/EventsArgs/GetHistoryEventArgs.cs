using System;
using System.Collections.Generic;

namespace Task3.EventsArgs
{
    public class GetHistoryEventArgs : EventArgs
    {
        public int IdPort { get; private set; }
        public DateTime Time { get; private set; }
        public List<string> History { get; private set; }

        public GetHistoryEventArgs(int id)
        {
            IdPort = id;
            Time = DateTime.Now;
        }

        public void SetHistory(List<string> list)
        {
            History = list;
        }
    }
}