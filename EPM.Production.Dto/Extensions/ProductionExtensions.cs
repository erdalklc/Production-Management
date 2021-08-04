using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EPM.Production.Dto.Extensions
{

    public static class ProductionExtensions
    {
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        public static string GetDescription<T>(this T enumerationValue)
            where T : struct
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type", nameof(enumerationValue));
            }
            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return enumerationValue.ToString();
        }
        public static DateTime AddBusinessDays(this DateTime current, int days)
        {
            var sign = Math.Sign(days);
            var unsignedDays = Math.Abs(days);
            for (var i = 0; i < unsignedDays; i++)
            {
                do
                {
                    current = current.AddDays(sign);
                } while (current.DayOfWeek == DayOfWeek.Saturday ||
                         current.DayOfWeek == DayOfWeek.Sunday);
            }
            return current;
        }
        public static int CalcBusinessDays(this DateTime current)
        {
            DateTime dtTemp = DateTime.Now;
            int days = Convert.ToInt32((dtTemp - current).TotalDays);
            int dayCount = 0;
            for (int i = 1; i < Math.Abs(days) + 1; i++)
            {
                dtTemp = dtTemp.AddDays(1);
                if (dtTemp.DayOfWeek != DayOfWeek.Saturday && dtTemp.DayOfWeek != DayOfWeek.Sunday)
                    dayCount++;
            }
            return dayCount;
        }
        public static string ToStringParse(this object value)
        {
            if (value == null)
                return "";
            string temp = "";
            try
            {
                temp = value.ToString();
            }
            catch (Exception)
            {

            }
            return temp;
        }
        public static int IntParse(this object value)
        {
            if (value == null)
                return 0;
            int temp = 0;
            Int32.TryParse(value.ToString(), out temp);
            return temp;
        }
        public static decimal DecimalParse(this object value)
        {
            if (value == null)
                return 0;
            decimal temp = 0;
            decimal.TryParse(value.ToString(), out temp);
            return temp;
        }
        public static float FloatParse(this object value)
        {
            if (value == null)
                return 0;
            float temp = 0;
            float.TryParse(value.ToString(), out temp);
            return temp;
        }
        public static bool BooleanParse(this object value)
        {
            if (value == null)
                return false;
            bool temp = false;
            bool.TryParse(value.ToString(), out temp);
            return temp;
        }
        public static DateTime DatetimeParse(this object value)
        {
            if (value == null)
                return DateTime.Now;

            DateTime temp = DateTime.Now;
            DateTime.TryParse(value.ToString(), out temp);
            return temp;
        }
        public static T FindOrDefault<T>(this List<T> t, Predicate<T> match)
        {
            T l = t.Find(match);
            if (l == null)
                return Activator.CreateInstance<T>();
            else return l;
        }

        public static string AyAdiGetir(this int Ay)
        {
            string ayAdi = "";
            switch (Ay)
            {
                case 1:
                    ayAdi = "OCAK";
                    break;
                case 2:
                    ayAdi = "ŞUBAT";
                    break;
                case 3:
                    ayAdi = "MART";
                    break;
                case 4:
                    ayAdi = "NİSAN";
                    break;
                case 5:
                    ayAdi = "MAYIS";
                    break;
                case 6:
                    ayAdi = "HAZİRAN";
                    break;
                case 7:
                    ayAdi = "TEMMUZ";
                    break;
                case 8:
                    ayAdi = "AGUSTOS";
                    break;
                case 9:
                    ayAdi = "EYLÜL";
                    break;
                case 10:
                    ayAdi = "EKİM";
                    break;
                case 11:
                    ayAdi = "KASIM";
                    break;
                case 12:
                    ayAdi = "ARALIK";
                    break;
                default:
                    break;
            }
            return ayAdi;
        }

        public static string InjectionString(this string text)
        {
            if (text == null) return "";
            return text.Replace("'", "").Replace("TRUNCATE", "").Replace("DELETE", "");
        }
    }
}
