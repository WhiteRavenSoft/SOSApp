using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using System.Text.RegularExpressions;
using System.ComponentModel;
using WhiteRaven.Core.ComponentModel;
using System.Globalization;
using System.Reflection;
using WhiteRaven.Core.DataObject;
using System.Web;
using System.Configuration;
using System.Net;
using WhiteRaven.Core.Enum;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace WhiteRaven.Core.Helper
{
    public static class AppHelper
    {
        public static CultureInfo ProviderAR = new CultureInfo("es-AR");
        public static CultureInfo ProviderUS = new CultureInfo("en-US");

        #region Application Key

        public const string SESSION_KEY = "WhiteRaven.SESSION_KEY";

        public const string SESSION_RESET_KEY = "WhiteRaven.SESSION_RESET_KEY";

        public const string SESSION_GUID_KEY = "WhiteRaven.SESSION_GUID_KEY";

        public const string LANG_KEY = "WhiteRaven.LANG_KEY";

        public const string CURRENCY_KEY = "WhiteRaven.CURRENCY_KEY";

        public const string CONTEXT_KEY = "WhiteRaven.CONTEXT_KEY";

        public const string PAGE_READONLY_KEY = "WhiteRaven.PAGE_READONLY_KEY";

        #endregion Application Key

        public static bool NotEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        #region Application Path

        public static string AppLocation(string path = null)
        {
            if (!path.NotEmpty())
                return LoadAppSetting("WhiteRaven.WebApp.Base.Path");

            return string.Format("{0}/{1}", LoadAppSetting("WhiteRaven.WebApp.Base.Path"), path);
        }

        public static string ApiLocation(string path = null)
        {
            if (!path.NotEmpty())
                return LoadAppSetting("WhiteRaven.WebApi.Base.Path");

            return string.Format("{0}/{1}", LoadAppSetting("WhiteRaven.WebApi.Base.Path"), path);
        }

        public static string CDNLocation(string path = null)
        {
            if (!path.NotEmpty())
                return LoadAppSetting("WhiteRaven.WebApp.CDN.Path");

            return string.Format("{0}/{1}", LoadAppSetting("WhiteRaven.WebApp.CDN.Path"), path);
        }


        public static bool AppOnTesting()
        {
            return LoadAppSettingboolean("WhiteRaven.WebApp.AppOnTesting", false);
        }

        public static bool AppLogger()
        {
            return LoadAppSettingboolean("WhiteRaven.WebApp.Logger.Enabled", false);
        }



        #endregion Application Path

        #region Setting

        public static int LoadAppSettingInteger(string name, int defaultValue)
        {
            string value = LoadAppSetting(name);
            if (!String.IsNullOrEmpty(value))
            {
                return int.Parse(value);
            }
            return defaultValue;
        }

        public static string LoadAppSetting(string name, string defaultValue = "")
        {
            if (String.IsNullOrEmpty(name))
                return null;

            if (ConfigurationManager.AppSettings[name].NotEmpty())
                return ConfigurationManager.AppSettings[name];

            return defaultValue;
        }

        public static decimal LoadAppSettingDecimalAR(string name, decimal defaultValue = decimal.Zero)
        {
            string value = LoadAppSetting(name);

            if (!String.IsNullOrEmpty(value))
            {
                return decimal.Parse(value, ProviderAR);
            }
            return defaultValue;
        }

        public static decimal LoadAppSettingDecimalUS(string name, decimal defaultValue = decimal.Zero)
        {
            string value = LoadAppSetting(name);

            if (!String.IsNullOrEmpty(value))
            {
                return decimal.Parse(value, ProviderUS);
            }
            return defaultValue;
        }

        public static bool LoadAppSettingboolean(string name, bool defaultValue = false)
        {
            string value = LoadAppSetting(name);
            if (!String.IsNullOrEmpty(value))
            {
                return bool.Parse(value);
            }

            return defaultValue;
        }

        #endregion Setting

        #region Security

        public static string CreateSaltKey(int size)
        {
            // Generate a cryptographic random number
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        public static string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1")
        {
            if (String.IsNullOrEmpty(passwordFormat))
                passwordFormat = "SHA1";
            string saltAndPassword = String.Concat(password, saltkey);

            //return FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPassword, passwordFormat);
            var algorithm = HashAlgorithm.Create(passwordFormat);
            if (algorithm == null)
                throw new ArgumentException("Unrecognized hash name");

            var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }

        public static string EncryptText(string plainText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            if (String.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = "encryptionPrivateKey";

            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            byte[] encryptedBinary = EncryptTextToMemory(plainText, tDESalg.Key, tDESalg.IV);
            return Convert.ToBase64String(encryptedBinary);
        }

        public static string DecryptText(string cipherText, string encryptionPrivateKey = "")
        {
            if (String.IsNullOrEmpty(cipherText))
                return cipherText;

            if (String.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = "encryptionPrivateKey";

            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            byte[] buffer = Convert.FromBase64String(cipherText);
            return DecryptTextFromMemory(buffer, tDESalg.Key, tDESalg.IV);
        }

        private static byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    byte[] toEncrypt = new UnicodeEncoding().GetBytes(data);
                    cs.Write(toEncrypt, 0, toEncrypt.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }

        private static string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    var sr = new StreamReader(cs, new UnicodeEncoding());
                    return sr.ReadLine();
                }
            }
        }

        #endregion Security

        public static string dotxtSiap(string lines, string Namefile)
        {

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"" + AppHelper.LoadAppSetting("WhiteRaven.WebTxt.Siap.Path") + Namefile + ".txt"))
            {
                file.WriteLine(lines);
            }

            return AppHelper.ApiLocation("TXT/" + Namefile + ".txt");
        }
        public static bool IsValidEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;

            email = email.Trim();
            var result = Regex.IsMatch(email, "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$", RegexOptions.IgnoreCase);
            return result;
        }

        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
                str = String.Concat(str, random.Next(10).ToString());
            return str;
        }

        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        public static string EnsureMaximumLength(string str, int maxLength, string postfix = null)
        {
            if (String.IsNullOrEmpty(str))
                return str;

            if (str.Length > maxLength)
            {
                var result = str.Substring(0, maxLength);
                if (!String.IsNullOrEmpty(postfix))
                {
                    result += postfix;
                }
                return result;
            }

            return str;
        }

        public static string EnsureNumericOnly(string str)
        {
            if (String.IsNullOrEmpty(str))
                return string.Empty;

            var result = new StringBuilder();
            foreach (char c in str)
            {
                if (Char.IsDigit(c))
                    result.Append(c);
            }
            return result.ToString();
        }

        public static string EnsureNotNull(string str)
        {
            if (str == null)
                return string.Empty;

            return str;
        }

        public static bool AreNullOrEmpty(params string[] stringsToValidate)
        {
            bool result = false;
            Array.ForEach(stringsToValidate, str =>
            {
                if (string.IsNullOrEmpty(str)) result = true;
            });
            return result;
        }

        public static bool ArraysEqual<T>(T[] a1, T[] a2)
        {
            //also see Enumerable.SequenceEqual(a1, a2);
            if (ReferenceEquals(a1, a2))
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            var comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < a1.Length; i++)
            {
                if (!comparer.Equals(a1[i], a2[i])) return false;
            }
            return true;
        }

        public static object To(object value, Type destinationType)
        {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }

        public static object To(object value, Type destinationType, CultureInfo culture)
        {
            if (value != null)
            {
                var sourceType = value.GetType();

                TypeConverter destinationConverter = GetCustomTypeConverter(destinationType);
                TypeConverter sourceConverter = GetCustomTypeConverter(sourceType);
                if (destinationConverter != null && destinationConverter.CanConvertFrom(value.GetType()))
                    return destinationConverter.ConvertFrom(null, culture, value);
                if (sourceConverter != null && sourceConverter.CanConvertTo(destinationType))
                    return sourceConverter.ConvertTo(null, culture, value, destinationType);
                if (destinationType.IsEnum && value is int)
                    return System.Enum.ToObject(destinationType, (int)value);
                if (!destinationType.IsInstanceOfType(value))
                    return Convert.ChangeType(value, destinationType, culture);
            }
            return value;
        }

        public static TypeConverter GetCustomTypeConverter(Type type)
        {
            //we can't use the following code in order to register our custom type descriptors
            //TypeDescriptor.AddAttributes(typeof(List<int>), new TypeConverterAttribute(typeof(GenericListTypeConverter<int>)));
            //so we do it manually here

            if (type == typeof(List<int>))
                return new GenericListTypeConverter<int>();
            if (type == typeof(List<decimal>))
                return new GenericListTypeConverter<decimal>();
            if (type == typeof(List<string>))
                return new GenericListTypeConverter<string>();
            if (type == typeof(Object))
                return new CustomOptionTypeConverter();
            if (type == typeof(List<Object>) || type == typeof(IList<Object>))
                return new CustomOptionListTypeConverter();

            return TypeDescriptor.GetConverter(type);
        }

        public static void SetProperty(object instance, string propertyName, object value)
        {
            if (instance == null) throw new ArgumentNullException("instance");
            if (propertyName == null) throw new ArgumentNullException("propertyName");

            Type instanceType = instance.GetType();
            PropertyInfo pi = instanceType.GetProperty(propertyName);
            if (pi == null)
                throw new WhiteRavenException("No property '{0}' found on the instance of type '{1}'.", propertyName, instanceType);
            if (!pi.CanWrite)
                throw new WhiteRavenException("The property '{0}' on the instance of type '{1}' does not have a setter.", propertyName, instanceType);
            if (value != null && !value.GetType().IsAssignableFrom(pi.PropertyType))
                value = To(value, pi.PropertyType);
            pi.SetValue(instance, value, new object[0]);
        }

        public static void SetCookie(HttpApplication app, string key, string val)
        {
            HttpCookie cookie = new HttpCookie(key);

            cookie.Value = val;

            if (string.IsNullOrEmpty(val))
            {
                cookie.Expires = DateTime.Now.AddMonths(-1);
            }
            else
            {
                cookie.Expires = DateTime.Now.AddHours(24);
            }

            app.Response.Cookies.Remove(key);
            app.Response.Cookies.Add(cookie);
        }

        public static void RemoveCookie(string key)
        {
            if (HttpContext.Current == null)
                return;

            if (HttpContext.Current.Request.Cookies[key] != null && HttpContext.Current.Request.Cookies[key].Value != null)
                HttpContext.Current.Request.Cookies.Remove(key);
        }

        public static string GetCookie(string key)
        {
            if (HttpContext.Current == null)
                return null;

            if (HttpContext.Current.Request.Cookies[key] != null && HttpContext.Current.Request.Cookies[key].Value != null)
            {
                var cookie = HttpContext.Current.Request.Cookies[key].Value;

                return HttpUtility.UrlDecode(cookie.ToString());
            }

            return null;
        }

        public static List<T> GetCookies<T>(string key)
        {
            var cookie = GetCookie(key);

            if (cookie != null)
                return JsonConvert.DeserializeObject<List<T>>(cookie);

            return default(List<T>);
        }

        public static T GetCookie<T>(string key)
        {
            var cookie = GetCookie(key);

            if (cookie != null)
                return JsonConvert.DeserializeObject<T>(cookie);

            return default(T);
        }

        public static string RemoveWhitespaceFromHtml(string html)
        {
            Regex RegexBetweenTags = new Regex(@">(?! )\s+", RegexOptions.Compiled);
            Regex RegexLineBreaks = new Regex(@"([\n\s])+?(?<= {2,})<", RegexOptions.Compiled);

            html = RegexBetweenTags.Replace(html, ">");
            html = RegexLineBreaks.Replace(html, "<");

            return html.Trim();
        }

        public static string EnumToText(this object value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            string description = value.ToString();
            FieldInfo fieldInfo = value.GetType().GetField(description);
            DescriptionAttribute[] attributes =
               (DescriptionAttribute[])
             fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }

            return description;
        }

        public static IDictionary<int, string> EnumToDictionary<TEnum>() where TEnum : struct
        {
            var enumerationType = typeof(TEnum);

            if (!enumerationType.IsEnum)
                throw new ArgumentException("Enumeration type is expected.");

            var dictionary = new Dictionary<int, string>();

            foreach (int x in System.Enum.GetValues(enumerationType))
            {
                TEnum val = (TEnum)System.Enum.ToObject(typeof(TEnum), x);
                var name = val.EnumToText();
                dictionary.Add(x, name);
            }

            return dictionary;
        }

        public static bool CheckForAlphaNum(this string str)
        {
            if (!str.NotEmpty())
                return false;

            return (str.ToCharArray().All(c => Char.IsLetter(c) || Char.IsNumber(c) || Char.IsWhiteSpace(c)));
        }

        public static bool CheckForLetterAndNumber(this string str)
        {
            str = str.Trim();

            if (!str.NotEmpty())
                return false;

            var hasLetter = str.ToCharArray().Any(c => Char.IsLetter(c));
            var hasNumber = str.ToCharArray().Any(c => Char.IsNumber(c));

            if (hasLetter && hasNumber)
                return true;

            return false;
        }

        public static bool CheckForNumber(this string str)
        {
            str = str.Trim();

            if (!str.NotEmpty())
                return false;

            var all = str.ToCharArray().All(c => Char.IsNumber(c));

            if (all)
                return true;

            return false;
        }

        public static bool ValidateTAXID(this string str, int length)
        {
            str = str.Trim();

            if (!str.NotEmpty() || str.Length > length)
                return false;

            return str.CheckForAlphaNum();
        }

        public static string ReadHtmlPage(string url)
        {
            StringBuilder sb = new StringBuilder();
            byte[] buf = new byte[8192];
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();

            string tempString = null;
            int count = 0;

            do
            {
                count = resStream.Read(buf, 0, buf.Length);

                if (count != 0)
                {
                    tempString = Encoding.UTF8.GetString(buf, 0, count);
                    sb.Append(tempString);
                }
            }
            while (count > 0);


            return sb.ToString();
        }

        public static bool ValidateIntPlaces(string val, int valid)
        {
            int length = 0;

            if (val.Contains('.') || val.Contains(','))
            {
                char[] separator = new char[] { '.', ',' };
                string[] tempstring = val.Split(separator);

                length = tempstring[0].Length;
            }

            return length <= valid;
        }

        public static bool ValidateDecimalPlaces(string val, int valid)
        {
            int length = 0;

            if (val.Contains('.') || val.Contains(','))
            {
                char[] separator = new char[] { '.', ',' };
                string[] tempstring = val.Split(separator);

                length = tempstring[1].Length;
            }

            return length <= valid;
        }

        public static string EnumDescription(this System.Enum value)
        {
            Type type = value.GetType();

            FieldInfo fieldInfo = type.GetField(value.ToString());

            EnumDescriptionAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(EnumDescriptionAttribute), false) as EnumDescriptionAttribute[];

            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }

        public static string EnumDescriptionLower(this System.Enum value)
        {
            Type type = value.GetType();

            FieldInfo fieldInfo = type.GetField(value.ToString());

            EnumDescriptionAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(EnumDescriptionAttribute), false) as EnumDescriptionAttribute[];

            return attribs.Length > 0 ? attribs[0].StringValue.ToLower() : null;
        }

        public static int ToInt(this object obj, int val = 0)
        {
            try
            {
                if (obj == null)
                    return val;

                return Convert.ToInt32(obj, ProviderUS);
            }
            catch
            {
                return val;
            }
        }

        public static long ToInt64(this object obj, int val = 0)
        {
            try
            {
                return Convert.ToInt64(obj, ProviderUS);
            }
            catch
            {
                return val;
            }
        }

        public static double ToDouble(this object obj, double val = 0)
        {
            try
            {
                return Math.Round(Convert.ToDouble(obj, ProviderUS), 2, MidpointRounding.AwayFromZero);
            }
            catch
            {
                return val;
            }
        }

        public static decimal ToDecimal(this object obj, decimal val = decimal.Zero)
        {
            try
            {
                return Convert.ToDecimal(obj, ProviderUS);
            }
            catch
            {
                return val;
            }
        }

     

        public static string ToDateStringAR(this DateTime obj)
        {
            try
            {
                return obj.ToString("dd/MM/yyyy");
            }
            catch
            {
                return StringResourcesEnum.Empty.EnumDescription();
            }
        }

        public static string ToDateStringAR(this DateTime? obj)
        {
            try
            {
                if (obj.HasValue)
                    return obj.Value.ToString("dd/MM/yyyy");
                return
                    StringResourcesEnum.Empty.EnumDescription();
            }
            catch
            {
                return StringResourcesEnum.Empty.EnumDescription();
            }
        }

        public static string ToDateTimeStringAR(this DateTime? obj)
        {
            try
            {
                if (obj.HasValue)
                    return obj.Value.ToString("dd/MM/yyyy HH:mm");
                return
                    StringResourcesEnum.Empty.EnumDescription();
            }
            catch
            {
                return StringResourcesEnum.Empty.EnumDescription();
            }
        }

        public static string ToDateTimeStringAR(this DateTime obj)
        {
            try
            {
                return obj.ToString("dd/MM/yyyy HH:mm");
            }
            catch
            {
                return StringResourcesEnum.Empty.EnumDescription();
            }
        }

        public static string ToDateTimeAirlineStringAR(this DateTime? obj)
        {
            try
            {
                if (obj.HasValue)
                    return obj.Value.ToString("dddd dd/MM/yyyy HH:mm", ProviderAR).Capitalize();
                return
                    StringResourcesEnum.Empty.EnumDescription();
            }
            catch
            {
                return StringResourcesEnum.Empty.EnumDescription();
            }
        }

        public static string ToDateTimeAirlineStringAR(this DateTime obj)
        {
            try
            {
                return obj.ToString("dddd dd/MM/yyyy HH:mm", ProviderAR).Capitalize();
            }
            catch
            {
                return StringResourcesEnum.Empty.EnumDescription();
            }
        }

        public static string Capitalize(this string text)
        {
            if (text.NotEmpty())
                return ProviderAR.TextInfo.ToTitleCase(text);

            return text;
        }

        public static string ToMoney(this object obj)
        {
            if (obj == null || decimal.Parse(obj.ToString()) == decimal.Zero)
                return "0";


            return string.Format("{0:##,##0.00}", obj);

            //return string.Format("{0:##,##0}", obj);
        }

        public static string ToMoneyG29(this object obj)
        {
            if (obj == null || decimal.Parse(obj.ToString()) == decimal.Zero)
                return "0";

            var num = NumberHelper.RoundUp(obj.ToDecimal(), 2);

            return num.ToString("G29", ProviderUS);

            //return string.Format("{0:##,##0}", obj);
        }

        public static string GetLast(this string source, int length)
        {
            if (length >= source.Length)
                return source;

            return source.Substring(source.Length - length);
        }

        public static string LoadIPAddress()
        {
            HttpContext httpContext = HttpContext.Current;

            if (httpContext != null)
            {
                string ipAddress = httpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!string.IsNullOrEmpty(ipAddress))
                {
                    string[] addresses = ipAddress.Split(',');
                    if (addresses.Length != 0)
                    {
                        return addresses[0];
                    }
                }

                if (httpContext.Request.ServerVariables["REMOTE_ADDR"] != null)
                    return httpContext.Request.ServerVariables["REMOTE_ADDR"];
            }

            return string.Empty;
        }

        public static string LimitString(this string str, int maxLength)
        {
            try
            {
                if (!str.NotEmpty())
                    return StringResourcesEnum.Empty.EnumDescription();

                if (str.Trim().Length > maxLength)
                    return string.Format("{0}..", str.Trim().Substring(0, maxLength));
                else
                    return str;
            }
            catch
            {
                return StringResourcesEnum.Empty.EnumDescription();
            }
        }

        public static string LimitHTML(this string html, int maxLength)
        {
            try
            {
                var str = StripHTML(html);

                if (!str.NotEmpty())
                    return StringResourcesEnum.Empty.EnumDescription();

                if (str.Trim().Length > maxLength)
                    return string.Format("{0}..", str.Trim().Substring(0, maxLength));
                else
                    return str;
            }
            catch
            {
                return StringResourcesEnum.Empty.EnumDescription();
            }
        }

        public static string LoadDigit(string code)
        {
            long cae = code.ToInt64();
            long cant = 0;
            long par = 0;
            long impar = 0;
            while (cae != 0)
            {
                if (cant % 2 == 0)
                {
                    par = par + (cae % 10);
                    cae = cae / 10;
                    cant++;
                }
                else
                {
                    impar = impar + (cae % 10);
                    cae = cae / 10;
                    cant++;
                }
            }
            cant = 0;
            impar = impar * 3;
            cant = par + impar;
            cant = cant % 10;
            cae = 10 - cant;
            return cae.ToString();
        }

        public static IQueryable<TEntity> DynamicOrderBy<TEntity>(this List<TEntity> source, string orderByProperty, bool desc) where TEntity : class
        {

            if (!orderByProperty.NotEmpty())
                return source.AsQueryable();

            string command = desc ? "OrderByDescending" : "OrderBy";

            var type = typeof(TEntity);

            var property = type.GetProperty(orderByProperty);

            var parameter = Expression.Parameter(type, "p");

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);

            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType }, source.AsQueryable().Expression, Expression.Quote(orderByExpression));

            return source.AsQueryable().Provider.CreateQuery<TEntity>(resultExpression);
        }

        public static string ConvertPlainTextToHtml(string text)
        {
            if (String.IsNullOrEmpty(text))
                return string.Empty;

            text = text.Replace("\r\n", "<br />");
            text = text.Replace("\r", "<br />");
            text = text.Replace("\n", "<br />");
            text = text.Replace("\t", "&nbsp;&nbsp;");
            text = text.Replace("  ", "&nbsp;&nbsp;");

            return text;
        }

        public static string ConvertHtmlToPlainText(string text)
        {
            return ConvertHtmlToPlainText(text, false);
        }

        public static string ConvertHtmlToPlainText(string text, bool decode)
        {
            if (String.IsNullOrEmpty(text))
                return string.Empty;

            if (decode)
                text = HttpUtility.HtmlDecode(text);

            text = text.Replace("<br>", "\n");
            text = text.Replace("<br >", "\n");
            text = text.Replace("<br />", "\n");
            text = text.Replace("&nbsp;&nbsp;", "\t");
            text = text.Replace("&nbsp;&nbsp;", "  ");

            return text;
        }

        public static string StripHTML(string html)
        {
            if (!html.NotEmpty())
                return html;

            return Regex.Replace(html, @"<.*?>", string.Empty, RegexOptions.Multiline);
        }

        //s/<[a-zA-Z\/][^>]*>//g

        //public static string StripHtmlTags(string text)
        //{
        //    if (!text.NotEmpty())
        //        return text;

        //    return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        //}

        public static string StripHtmlInput(string text)
        {
            return Regex.Replace(text, @"<input (.|\n)*?>", string.Empty);
        }

        public static string RenderHtml(this HtmlGenericControl htmlControl)
        {
            string result = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                var writer = new HtmlTextWriter(sw);
                htmlControl.RenderControl(writer);
                result = sw.ToString();
                writer.Close();
            }

            return result;
        }

        public static string ToStringNull(this object obj)
        {
            var str = string.Empty;

            if (!(obj is DBNull) && obj != null)
                str = obj.ToString();

            return str;
        }

        public static int ToIntNull(this object obj, int val = 0)
        {
            try
            {
                if (!(obj is DBNull) && obj != null)
                {
                    return Convert.ToInt32(obj, ProviderUS);
                }
                else
                    return val;
            }
            catch
            {
                return val;
            }
        }

        #region Validar CUIT

        public static int CalcularDigitoCuit(string cuit)
        {
            int[] mult = new int[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };

            char[] nums = cuit.ToCharArray();
            int total = 0;
            for (int i = 0; i < mult.Length; i++)
            {
                total += int.Parse(nums[i].ToString()) * mult[i];
            }
            int resto = total % 11;
            return resto == 0 ? 0 : resto == 1 ? 9 : 11 - resto;
        }

        public static bool ValidaCUIT(string cuit)
        {
            if (cuit == null)
            {
                return false;
            }

            cuit = cuit.Replace("-", string.Empty);

            if (cuit.Length != 11)
            {
                return false;
            }
            else
            {
                int calculado = CalcularDigitoCuit(cuit);
                int digito = int.Parse(cuit.Substring(10));
                return calculado == digito;
            }
        }

        #endregion Validar CUIT
    }
}