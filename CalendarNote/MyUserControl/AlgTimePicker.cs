using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarNote.MyUserControl
{
    class AlgTimePicker
    {
        private const int NUMOFDAY = 7;
        private const int NUMOFWEEK = 6;

        public static int daysIn(int month, int year)
        {
            return new DateTime(year, month, 1).AddMonths(1).AddDays(-1).Day;
        }

        private static int getDayofWeek(DayOfWeek day)
        {
            return (int)(day);
        }

        public static List<DateTime> calendar(int month, int year)
        {
            DateTime dt = new DateTime(year, month, 1);
            DayOfWeek day = dt.DayOfWeek;

            int startIndex = getDayofWeek(day);
            int endIndex = daysIn(month, year);
            List<DateTime> result = new List<DateTime>();

            for (int i = 0; i < NUMOFDAY * NUMOFWEEK; i++)
                result.Add(new DateTime());

            for (var i = startIndex; i < endIndex + startIndex; i++)
                result[i] = new DateTime(year, month, (i - startIndex) + 1);

            int preMonth = (month - 1 < 1) ? 12 : month - 1;
            int yearOfPreMonth = (month - 1 < 1) ? year - 1 : year;
            int nexMonth = (month + 1 > 12) ? 1 : month + 1;
            int yearOfNexMonth = (month + 1 > 12) ? year + 1 : year;

            int numOfDayPreMonth = daysIn(preMonth, yearOfPreMonth);

            for (int i = startIndex - 1; i >= 0; i--)
                result[i] = new DateTime(year, preMonth, numOfDayPreMonth--);

            int indexNexMonth = 1;
            for (int i = endIndex + startIndex; i < NUMOFDAY * NUMOFWEEK; i++)
                result[i] = new DateTime(year, nexMonth, indexNexMonth++);
            return result;
        }

        public static List<DateTime> calendar(DateTime date)
        {
            int mon = date.Month;
            int year = date.Year;
            return calendar(mon, year);
        }

        public static void ConvertListtoArray(List<DateTime> a, ref DateTime[][] arr_Calendar)
        {
            int index = 0;
            for (int i = 0; i < NUMOFWEEK; i++)
                for (int j = 0; j < NUMOFDAY; j++)
                    arr_Calendar[i][j] = a[index++];
        }

    }
}
