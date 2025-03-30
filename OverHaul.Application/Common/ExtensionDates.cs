using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application;

public static class ExtensionDates
{
    #region string
    public static DateOnly JalaliToGregorian(this string I_DATE)
    {
        I_DATE = I_DATE.Fa2En();
        PersianCalendar x = new PersianCalendar();
        int Y = Convert.ToInt16(I_DATE.Substring(0, 4));
        int M = Convert.ToInt16(I_DATE.Substring(5, 2));
        int D = Convert.ToInt16(I_DATE.Substring(8, 2));
        DateOnly dt = DateOnly.FromDateTime(x.ToDateTime(Y, M, D, 0, 0, 0, 1));
        return dt;
    }
    public static string GregorianToJalali(this string I_DATE)
    {
        DateTime DateGregorian = Convert.ToDateTime(I_DATE);
        PersianCalendar p = new PersianCalendar();
        return string.Format(@"{0}/{1}/{2}",
             p.GetYear(DateGregorian),
             p.GetMonth(DateGregorian).ToString().PadLeft(2, '0'),
             p.GetDayOfMonth(DateGregorian).ToString().PadLeft(2, '0'));
    }
    public static DateTime JalaliToGregorian(this string I_DATE, string I_TIME = "" /*23:59:59*/)
    {
        I_DATE= I_DATE.Fa2En();
        PersianCalendar x = new PersianCalendar();
        int Y = Convert.ToInt16(I_DATE.Substring(0, 4));
        int M = Convert.ToInt16(I_DATE.Substring(5, 2));
        int D = Convert.ToInt16(I_DATE.Substring(8, 2));
        int H24 = 0;
        int MI = 0;
        int SS = 0;
        int count = I_TIME.Count(f => f == ':');
        if (count == 2)
        {
            string[] timeArray = I_TIME.Split(':');
            H24 = Convert.ToInt16(timeArray[0]);
            MI = Convert.ToInt16(timeArray[1]);
            SS = Convert.ToInt16(timeArray[2]);
        }
        else if (count == 1)
        {
            string[] timeArray = I_TIME.Split(':');
            H24 = Convert.ToInt16(timeArray[0]);
            MI = Convert.ToInt16(timeArray[1]);
            SS = 0;
        }

        DateTime dt = x.ToDateTime(Y, M, D, H24, MI, SS, 0, 0);
        return dt;


    }
    public static DateOnly JalaliToGregorianDateOnly(this string I_DATE)
    {
        I_DATE = I_DATE.Fa2En();
        PersianCalendar x = new PersianCalendar();
        int Y = Convert.ToInt16(I_DATE.Substring(0, 4));
        int M = Convert.ToInt16(I_DATE.Substring(5, 2));
        int D = Convert.ToInt16(I_DATE.Substring(8, 2));
        DateTime dt = x.ToDateTime(Y, M, D, 0, 0, 0, 0, 0);
        return DateOnly.FromDateTime(dt);
    }
    public static DateOnly GetNowGregorianDateOnly(this string I_DATE)
    {
        PersianCalendar x = new PersianCalendar();
        int Y = Convert.ToInt16(DateTime.Now);
        int M = Convert.ToInt16(DateTime.Now);
        int D = Convert.ToInt16(DateTime.Now);
        DateTime dt = x.ToDateTime(Y, M, D, 0, 0, 0, 0, 0);
        return DateOnly.FromDateTime(dt);
    }

    public static DateOnly StringToDateOnly(this string I_DATE)
    {
        DateTime firstdate = DateTime.ParseExact(I_DATE,"yyyy/MM/dd",CultureInfo.InvariantCulture);

        DateOnly dt = DateOnly.FromDateTime(firstdate);
        return dt;
    }
    public static string GetTime(this string I_DATE)
    {
        DateTime date = Convert.ToDateTime(I_DATE);
        TimeSpan time = date.DateTimeToTimeOnly();
        PersianCalendar p = new PersianCalendar();
        return time.ToString();
    }

    #endregion

    #region DateTime
    public static DateOnly DateTimeToDateOnly(this DateTime I_DATE)
    {
        DateOnly dt = DateOnly.FromDateTime(I_DATE);
        return dt;
    }
    public static TimeSpan DateTimeToTimeOnly(this DateTime I_DATE)
    {
        TimeSpan dt = TimeOnly.FromDateTime(I_DATE).ToTimeSpan();
        return dt;
    }

    public static string TimeSpanFormat(this TimeSpan I_TiME)
    {
        return I_TiME.ToString(@"hh\:mm"); 
    }

    public static int GetJalaliMounthNumber(this DateTime I_DATE)
    {
        PersianCalendar p = new PersianCalendar();
        int month = p.GetMonth(I_DATE);
        return month;
    }
    public static string GetJalaliMounthName(this DateTime I_DATE)
    {
        PersianCalendar p = new PersianCalendar();
        int month = p.GetMonth(I_DATE);
        switch (month)
        {
            case 1:
                return "فروردین";
            case 2:
                return "اردیبهشت";
            case 3:
                return "خرداد";
            case 4:
                return "تیر";
            case 5:
                return "مرداد";
            case 6:
                return "شهریور";
            case 7:
                return "مهر";
            case 8:
                return "آبان";
            case 9:
                return "آذر";
            case 10:
                return "دی";
            case 11:
                return "بهمن";
            case 12:
                return "اسفند";
            default:
                return "";
        };

    }

    // از تاریخ ارسال شده ماه شمسی را برداشته و تاریخ روز آخر آن ماه را برمیگرداند
    // برای مثال اگر تاریخ 1403/06/15 را بفرستیم تاریخ 1403/06/31 را برمیگرداند
    public static string GetJalaliDateByJalaliMount_EndDate(this DateTime I_DATE)
    {
        PersianCalendar p = new PersianCalendar();
        int month = p.GetMonth(I_DATE);
        int year = p.GetYear(I_DATE);
        int FinalDay = p.GetDaysInMonth(year, month);

        return string.Format(@"{0}/{1}/{2}",
             year,
             month.ToString().PadLeft(2, '0'),
             FinalDay.ToString().PadLeft(2, '0'));
    }
    // از تاریخ ارسال شده ماه شمسی را برداشته و تاریخ روز اول آن ماه را برمیگرداند
    // برای مثال اگر تاریخ 1403/06/15 را بفرستیم تاریخ 1403/06/01 را برمیگرداند
    public static string GetJalaliDateByJalaliMount_StartDate(this DateTime I_DATE)
    {
        PersianCalendar p = new PersianCalendar();
        int month = p.GetMonth(I_DATE);
        int year = p.GetYear(I_DATE);
        int startday = 1;

        return string.Format(@"{0}/{1}/{2}",
             year,
             month.ToString().PadLeft(2, '0'),
             startday.ToString().PadLeft(2, '0'));
    }

    public static string GregorianToJalali(this DateTime? I_DATE)
    {    DateTime dt = I_DATE.HasValue ? (DateTime)I_DATE :  DateTime.Now;
        if (I_DATE.HasValue)
        {
            PersianCalendar p = new PersianCalendar();
            return string.Format(@"{0}/{1}/{2}",
                 p.GetYear(dt),
                 p.GetMonth(dt).ToString().PadLeft(2, '0'),
                 p.GetDayOfMonth(dt).ToString().PadLeft(2, '0'));
        }
        else
            return "";
    }
    public static string GregorianToJalali(this DateTime I_DATE)
    {
      
            PersianCalendar p = new PersianCalendar();
            return string.Format(@"{0}/{1}/{2}",
                 p.GetYear(I_DATE),
                 p.GetMonth(I_DATE).ToString().PadLeft(2, '0'),
                 p.GetDayOfMonth(I_DATE).ToString().PadLeft(2, '0'));
        
    }



    #endregion

    #region DateOnly
    public static string GregorianToJalali(this DateOnly I_DATE)
    {
        DateTime DateGregorian = I_DATE.ToDateTime(TimeOnly.FromDateTime(DateTime.Now));

        PersianCalendar p = new PersianCalendar();
        return string.Format(@"{0}/{1}/{2}",
             p.GetYear(DateGregorian),
             p.GetMonth(DateGregorian).ToString().PadLeft(2, '0'),
             p.GetDayOfMonth(DateGregorian).ToString().PadLeft(2, '0'));
    }

    public static DateTime MergerDateOnlyAndTimeSpan(this DateOnly I_DATE , DateOnly date , TimeSpan time)
    {
        DateTime newDatetime = date.ToDateTime(TimeOnly.FromTimeSpan(time));

        return newDatetime;
    }



    #endregion
    #region OtherType


    #endregion


}
