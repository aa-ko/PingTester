using System;

namespace PingTester
{
    public class UrlControl
    {
        public String Url { get; private set; }
        public String DisplayName { get; private set; }
        public bool IsActive { get; set; }

        public Int32 PingInterval { get; private set; }
        public Int32 HistorySize { get; private set; }

        public UrlControl(String url,
                          String displayName,
                          Int32 pingInterval,
                          Int32 historySize)
        {
            Url = url;
            DisplayName = displayName;
            PingInterval = pingInterval;
            HistorySize = HistorySize;

            IsActive = false;
        }

        public override string ToString()
        {
            return String.Format($"{DisplayName} ({Url})");
        }
    }
}
