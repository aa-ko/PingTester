using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingTester
{
    public static class ListExtensions
    {
        public static List<T> AddToListAsQueue<T>(this List<T> list, T addition, int maxCount)
        {
            while (list.Count > maxCount - 1)
            {
                list.Remove(list.First());
            }
            list.Add(addition);
            return list;
        }
    }
}
