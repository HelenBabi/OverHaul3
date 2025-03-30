using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Overhaul.Application.Public;

public class PersianDate
{
    public static DateTime ShamsiTomiladiWtime(string I_DATE, string I_TIME)
    {
        System.Globalization.PersianCalendar x = new System.Globalization.PersianCalendar();
        int Y = Convert.ToInt16(I_DATE.Substring(0, 4));
        int M = Convert.ToInt16(I_DATE.Substring(5, 2));
        int D = Convert.ToInt16(I_DATE.Substring(8, 2));
        string[] timeArray = I_TIME.Split(':');
        int H24 = Convert.ToInt16(timeArray[0]);
        int MI = Convert.ToInt16(timeArray[1]);
        DateTime dt = x.ToDateTime(Y, M, D, H24, MI, 0, 0, 0);
        return dt;
    }
    public static DateTime ShamsiTomiladiTime000001(string I_DATE)
    {

        System.Globalization.PersianCalendar x = new System.Globalization.PersianCalendar();
        int Y = Convert.ToInt16(I_DATE.Substring(0, 4));
        int M = Convert.ToInt16(I_DATE.Substring(5, 2));
        int D = Convert.ToInt16(I_DATE.Substring(8, 2));

        DateTime dt = x.ToDateTime(Y, M, D, 0, 0, 0, 1);
        return dt;
    }
    public static DateTime ShamsiTomiladiTime235959(string I_DATE)
    {

        System.Globalization.PersianCalendar x = new System.Globalization.PersianCalendar();
        int Y = Convert.ToInt16(I_DATE.Substring(0, 4));
        int M = Convert.ToInt16(I_DATE.Substring(5, 2));
        int D = Convert.ToInt16(I_DATE.Substring(8, 2));

        DateTime dt = x.ToDateTime(Y, M, D, 23, 59, 59, 999);
        return dt;
    }
    public static string getPersianDate()
    {
        PersianCalendar PDate = new PersianCalendar();
        string y, m, d;
        y = Convert.ToString(PDate.GetYear(DateTime.Now));
        m = Convert.ToString(PDate.GetMonth(DateTime.Now));
        if (m.Length == 1)
            m = "0" + m;
        d = Convert.ToString(PDate.GetDayOfMonth(DateTime.Now));
        if (d.Length == 1)
            d = "0" + d;
        return y + "/" + m + "/" + d;
    }
    public static string getPersianDateLastDay(int LastDay)
    {
        DateTime calcDay = DateTime.Now.AddDays((-1) * LastDay);
        PersianCalendar PDate = new PersianCalendar();
        string y, m, d;
        y = Convert.ToString(PDate.GetYear(calcDay));
        m = Convert.ToString(PDate.GetMonth(calcDay));
        if (m.Length == 1)
            m = "0" + m;
        d = Convert.ToString(PDate.GetDayOfMonth(calcDay));
        if (d.Length == 1)
            d = "0" + d;
        return y + "/" + m + "/" + d;
    }
    public static int getPersianTodayNumber(int LastDay)
    {
        DateTime calcDay = DateTime.Now.AddDays((-1) * LastDay);
        PersianCalendar PDate = new PersianCalendar();
        string y, m, d;
        y = Convert.ToString(PDate.GetYear(calcDay));
        m = Convert.ToString(PDate.GetMonth(calcDay));
        if (m.Length == 1)
            m = "0" + m;
        d = Convert.ToString(PDate.GetDayOfMonth(calcDay));
        if (d.Length == 1)
            d = "0" + d;
        return Convert.ToInt32(y + m + d);
    }
    public static string getSessionDate()
    {
        PersianCalendar PDate = new PersianCalendar();
        string y, m, d;
        y = Convert.ToString(PDate.GetYear(DateTime.Now));
        m = Convert.ToString(PDate.GetMonth(DateTime.Now));
        if (m.Length == 1)
            m = "0" + m;
        d = Convert.ToString(PDate.GetDayOfMonth(DateTime.Now));
        if (d.Length == 1)
            d = "0" + d;
        return y + m + d;
    }
    public static string getPersianDate(DateTime curDateTime)
    {
        PersianCalendar PDate = new PersianCalendar();
        string y, m, d;
        y = Convert.ToString(PDate.GetYear(curDateTime));
        m = Convert.ToString(PDate.GetMonth(curDateTime));
        if (m.Length == 1)
            m = "0" + m;
        d = Convert.ToString(PDate.GetDayOfMonth(curDateTime));
        if (d.Length == 1)
            d = "0" + d;
        return y + "/" + m + "/" + d;
    }
    public static string getPersianDate(DateTime? curDateTime)
    {
        if (curDateTime == null)
        {
            return "";
        }
        PersianCalendar PDate = new PersianCalendar();
        string y, m, d;
        y = Convert.ToString(PDate.GetYear(curDateTime.Value));
        m = Convert.ToString(PDate.GetMonth(curDateTime.Value));
        if (m.Length == 1)
            m = "0" + m;
        d = Convert.ToString(PDate.GetDayOfMonth(curDateTime.Value));
        if (d.Length == 1)
            d = "0" + d;
        return y + "/" + m + "/" + d;
    }
    public static string getPersianDateTime(DateTime curDateTime)
    {
        PersianCalendar PDate = new PersianCalendar();
        string y, m, d;
        y = Convert.ToString(PDate.GetYear(curDateTime));
        m = Convert.ToString(PDate.GetMonth(curDateTime));
        if (m.Length == 1)
            m = "0" + m;
        d = Convert.ToString(PDate.GetDayOfMonth(curDateTime));
        if (d.Length == 1)
            d = "0" + d;
        return y + "/" + m + "/" + d + " " + curDateTime.ToString("HH:mm:ss");
    }

    public static string getPersianDateTimeRtl(DateTime curDateTime)
    {
        PersianCalendar PDate = new PersianCalendar();
        string y, m, d;
        y = Convert.ToString(PDate.GetYear(curDateTime));
        m = Convert.ToString(PDate.GetMonth(curDateTime));
        if (m.Length == 1)
            m = "0" + m;
        d = Convert.ToString(PDate.GetDayOfMonth(curDateTime));
        if (d.Length == 1)
            d = "0" + d;
        return curDateTime.ToString("HH:mm:ss") + " " + y + "/" + m + "/" + d + " ";
    }
    public static string getPersianDateTime()
    {
        DateTime curDateTime = DateTime.Now;
        PersianCalendar PDate = new PersianCalendar();
        string y, m, d;
        y = Convert.ToString(PDate.GetYear(curDateTime));
        m = Convert.ToString(PDate.GetMonth(curDateTime));
        if (m.Length == 1)
            m = "0" + m;
        d = Convert.ToString(PDate.GetDayOfMonth(curDateTime));
        if (d.Length == 1)
            d = "0" + d;
        return y + "_" + m + "_" + d + "_" + curDateTime.ToString("HH-mm-ss");
    }
    public static string getPersianDateTimeCalendarInput(int afterMinute)
    {
        DateTime curDateTime = DateTime.Now;
        PersianCalendar PDate = new PersianCalendar();
        string y, m, d;
        y = Convert.ToString(PDate.GetYear(curDateTime));
        m = Convert.ToString(PDate.GetMonth(curDateTime));
        if (m.Length == 1)
            m = "0" + m;
        d = Convert.ToString(PDate.GetDayOfMonth(curDateTime));
        if (d.Length == 1)
            d = "0" + d;
        return y + "/" + m + "/" + d + " " + curDateTime.AddMinutes(afterMinute).ToString("HH:mm:ss");
    }
    public static string getPersianDateF()
    {
        PersianCalendar PDate = new PersianCalendar();
        string y, m, d;
        y = Convert.ToString(PDate.GetYear(DateTime.Now));
        m = Convert.ToString(PDate.GetMonth(DateTime.Now));
        if (m.Length == 1)
            m = "0" + m;
        d = Convert.ToString(PDate.GetDayOfMonth(DateTime.Now));
        if (d.Length == 1)
            d = "0" + d;
        return d + "/" + m + "/" + y;
    }
    
    //public static string getHurufiDate()
    //{
    //    Persia.SunDate SunDate = Persia.Calendar.ConvertToPersian(DateTime.Now);
    //    return SunDate.Persian;
    //}
    //public static string getHurufiDate(DateTime curDateTime)
    //{
    //    Persia.SunDate SunDate = Persia.Calendar.ConvertToPersian(curDateTime);
    //    return SunDate.Persian;
    //}
    public static string reverseDate(string strDate)
    {
        strDate = strDate.Trim();
        if (strDate.Substring(4, 1) == "/")
            return (strDate.Substring(8) + "/" + strDate.Substring(5, 2) + "/" + strDate.Substring(0, 4));

        else if (strDate.Substring(2, 1) == "/")
            return (strDate.Substring(6) + "/" + strDate.Substring(3, 2) + "/" + strDate.Substring(0, 2));
        else
            return "";

    }
    //public static string ShamsiTomiladiTime000001(string I_DATE)
    //{

    //    System.Globalization.PersianCalendar x = new System.Globalization.PersianCalendar();
    //    int Y = Convert.ToInt16(I_DATE.Substring(0, 4));
    //    int M = Convert.ToInt16(I_DATE.Substring(5, 2));
    //    int D = Convert.ToInt16(I_DATE.Substring(8, 2));

    //    DateTime dt = x.ToDateTime(Y, M, D, 0, 0, 0, 1);
    //    return dt;
    //}

}
