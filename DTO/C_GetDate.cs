using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppNetShop.DTO
{
    public class C_GetDate
    {
        public static DateTime FirstDayMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }

        public static DateTime LastDayMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }

        public static DateTime BeforeDayMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddDays((-dtResult.Day));
            return dtResult;
        }

        public static int SumMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult.Day;
        }

        public static List<DateTime?> GetAllDates(DateTime? startDate, DateTime? endDate)
        {
            List<DateTime?> dates = new List<DateTime?>();
            DateTime? currentDate = startDate;

            while (currentDate <= endDate)
            {
                dates.Add(currentDate);
                currentDate = currentDate?.AddDays(1);
            }
            return dates;
        }

    }
}