using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace PingTester
{
    public class PingService
    {
        private const Int32 HistorySize = 20;
        private const Int32 Timeout = 2000;

        private Dictionary<String, List<PingReply>> _pingHistory;

        public PingService()
        {
            _pingHistory = new Dictionary<String, List<PingReply>>();
        }

        public void IssueNewPingIfActive(UrlControl url)
        {
            if (url == null) return;
            if (url.IsActive) IssueNewPing(url.Url);
        }

        public async void IssueNewPing(String url)
        {
            if (!_pingHistory.ContainsKey(url)) _pingHistory.Add(url, new List<PingReply>());

            using (var ping = new Ping())
            {
                var reply = await ping.SendPingAsync(url, Timeout);
                List<PingReply> pings;
                _pingHistory.TryGetValue(url, out pings);
                pings.AddToListAsQueue(reply, HistorySize);
            }
        }

        public long GetAveragePing(UrlControl url)
        {
            return GetAveragePing(url.Url);
        }

        public long GetAveragePing(String url)
        {
            if (!_pingHistory.TryGetValue(url, out List<PingReply> pings)) return -1;
            if (pings.Count <= 0) return -1;

            var successPings = pings.Where(p => p.Status == IPStatus.Success);
            var timedOutPings = pings.Where(p => p.Status == IPStatus.TimedOut);
            var ignoredPings = pings.Where(p => !(p.Status == IPStatus.Success || p.Status == IPStatus.TimedOut));

            if (successPings.Count() == 0 && timedOutPings.Count() > 0) return -1;

            return   (successPings.Select(p => p.RoundtripTime).Sum()
                   + Timeout * timedOutPings.Count())
                   / (successPings.Count() + timedOutPings.Count());
        }
    }
}
