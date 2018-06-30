using SOSApp.Svc.DataService;
using SOSApp.Svc.Infrastructure;
using SOSApp.Core.Enum;
using SOSApp.Core.Helper;
using SOSApp.Data.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SOSApp.Svc.Helper
{
    public static class RateHelper
    {
        public static decimal AddMarkup(this decimal amount, decimal? markup)
        {
            if (markup.HasValue && markup.Value != decimal.Zero)
                return Math.Round(amount / markup.Value, 2);

            return amount;
        }

        public static decimal AddMarkup(this decimal amount, decimal markup)
        {
            if (markup != decimal.Zero)
                return Math.Round(amount / markup, 2);

            return amount;
        }

        public static List<int> ParseRooming(string data)
        {
            var all = new List<int>();

            var paxes = Regex.Split(data, "\\.");

            foreach (var x in paxes)
            {
                var sec = Regex.Split(x, "\\-");

                if (sec.Count() > 1)
                {
                    var first = sec.First().ToInt();
                    var last = sec.Last().ToInt();

                    if (last != first)
                    {
                        for (var a = first; a <= last; a++)
                        {
                            all.Add(a);
                        }
                    }
                }
                else
                    all.Add(x.ToInt());
            }

            return all;
        }
    }
}
