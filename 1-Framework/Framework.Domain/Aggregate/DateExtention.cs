using System;

namespace Framework.Domain.Aggregate
{
    public static class DateExtention
    {
        public static long ToPersianTime(this DateTime dateTime)
        {
            var time = dateTime.TimeOfDay;
            return time.Hours*10000000 + time.Minutes*100000 + time.Seconds*1000 + time.Milliseconds;
        }


        public static long ToPersianDate(this DateTime dateTime)
        {
            var p = new System.Globalization.PersianCalendar();
            var year = p.GetYear(dateTime);
            var month = p.GetMonth(dateTime);
            var day = p.GetDayOfMonth(dateTime);
            return year * 10000 + month * 100 + day;
        }


        public static string ToPersianDateFormat(this int persianDate)
        {
            return string.Format("{0:0000}/{1:00}/{2:00}", persianDate / 10000, ((persianDate % 10000) / 100), persianDate % 100);
        }

        public static string ToPersianDateFormat(this int? persianDate)
        {
            return string.Format("{0:0000}/{1:00}/{2:00}", persianDate / 10000, ((persianDate % 10000) / 100), persianDate % 100);
        }

        public static string ToPersianTimeFormat(this int persianTime)
        {
            return string.Format("{0:00}:{1:00}:{2:00}", persianTime / 10000, (persianTime / 100) % 100, persianTime % 100);
        }

        public static string ToPersianTimeFormat(this int? persianTime)
        {
            return string.Format("{0:00}:{1:00}:{2:00}", persianTime / 10000, (persianTime / 100) % 100, persianTime % 100);
        }
    }
}