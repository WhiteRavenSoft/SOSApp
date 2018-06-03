using System;
using System.Globalization;

namespace WhiteRaven.Core.Helper
{
    public static class DateHelper
    {
        public static CultureInfo ProviderAR = new CultureInfo("es-AR");
        public static CultureInfo ProviderUS = new CultureInfo("en-US");

        public static DateTime ToDateUS(this object obj)
        {
            try
            {
                return DateTime.ParseExact(obj.ToString(), "dd/MM/yyyy", ProviderUS);
                //return Convert.ToDateTime(obj, ProviderUS);
            }
            catch
            {
                return DateTime.Today;
            }
        }

        public static DateTime ToDateAR(this object obj)
        {
            try
            {
                return DateTime.ParseExact(obj.ToString(), "dd/MM/yyyy", ProviderAR);
                //Convert.ToDateTime(obj, ProviderAR);
            }
            catch
            {
                return DateTime.Today;
            }
        }

        public static string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = AppHelper.ProviderAR.DateTimeFormat;

            return dtinfo.GetMonthName(month).ToUpper();
        }

        public static int GetMonthInt(string month)
        {
            int mes = 0;
            switch (month.Trim().ToUpper())
            {
                case "ENERO":
                    mes = 1;
                    break;
                case "FEBRERO":
                    mes = 2;
                    break;
                case "MARZO":
                    mes = 3;
                    break;
                case "ABRIL":
                    mes = 4;
                    break;
                case "MAYO":
                    mes = 5;
                    break;
                case "JUNIO":
                    mes = 6;
                    break;
                case "JULIO":
                    mes = 7;
                    break;
                case "AGOSTO":
                    mes = 8;
                    break;
                case "SEPTIEMBRE":
                    mes = 9;
                    break;
                case "OCTUBRE":
                    mes = 10;
                    break;
                case "NOVIEMBRE":
                    mes = 11;
                    break;
                case "DICIEMBRE":
                    mes = 12;
                    break;
            }

            return mes;
        }
    }
}
