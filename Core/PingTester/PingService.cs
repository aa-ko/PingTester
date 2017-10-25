using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace PingTester
{
    public class PingService
    {
        private const Int32 HistorySize = 20;

        private Dictionary<String, List<PingReply>> _pingHistory;

        public PingService()
        {
            _pingHistory = new Dictionary<String, List<PingReply>>();
        }

        public async void IssueNewPing(String url)
        {
            if (!_pingHistory.ContainsKey(url)) _pingHistory.Add(url, new List<PingReply>());

            using (var ping = new Ping())
            {
                var reply = await ping.SendPingAsync(url);
                List<PingReply> pings;
                _pingHistory.TryGetValue(url, out pings);
                pings.AddToListAsQueue(reply, HistorySize);
            }
        }

        public int GetAveragePing(String url)
        {
            List<PingReply> pings;
            if (_pingHistory.TryGetValue(url, out pings)) return -1;

            return pings.Select(p => p.RoundtripTime)
                        .Select(r => (Int32)r)
                        .Aggregate((a, b) => a + (b * (1 / HistorySize)));
        }
    }
}
