using WhiteRaven.Svc.DataService;
using WhiteRaven.Svc.Infrastructure;
using WhiteRaven.Core.Enum;
using WhiteRaven.Core.Helper;
using WhiteRaven.Data.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WhiteRaven.Svc.Helper
{
    public static class RateHelper
    {
        public static CurrencyEnum ConvertCurrency(int currencyid)
        {
            var resp = CurrencyEnum.USD;

            if (currencyid != 0)
            {
                if (currencyid == AppHelper.LoadAppSettingInteger("WhiteRaven.Currency.ARS", 8))
                    resp = CurrencyEnum.ARS;

                //if (currencyid == AppHelper.LoadAppSettingInteger("WhiteRaven.Currency.RAL", 9))
                //    resp = CurrencyEnum.RAL;
            }

            return resp;
        }

        public static int ConvertCurrency(CurrencyEnum currency)
        {
            var resp = AppHelper.LoadAppSettingInteger("WhiteRaven.Currency.USD", 1);

            switch (currency)
            {
                case CurrencyEnum.ARS:
                    resp = AppHelper.LoadAppSettingInteger("WhiteRaven.Currency.ARS", 8);
                    break;

                //case CurrencyEnum.RAL:
                //    resp = AppHelper.LoadAppSettingInteger("WhiteRaven.Currency.RAL", 9);
                //    break;
            }

            return resp;
        }

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

        public static int PaxCount(RateTypeEnum rateType)
        {
            var count = 1;

            switch (rateType)
            {
                case RateTypeEnum.Doble:
                    count = 2;
                    break;

                case RateTypeEnum.Triple:
                    count = 3;
                    break;
            }

            return count;
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
