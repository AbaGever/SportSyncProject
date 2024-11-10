using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models2
{
    using System;

    public class time
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        // Constructor with parameters
        public time(int day, int month, int year)
        {
            this.Day = day;
            this.Month = month;
            this.Year = year;
        }

        // Default constructor sets to the current date
        public time()
        {
            var currentDate = DateTime.Now;
            this.Day = currentDate.Day;
            this.Month = currentDate.Month;
            this.Year = currentDate.Year;
        }

        // Parse method without using Split or parts
        public static time Parse(string dateStr)
        {
            int day = int.Parse(dateStr.Substring(0, 2));
            int month = int.Parse(dateStr.Substring(3, 2));
            int year = int.Parse(dateStr.Substring(6, 4));

            return new time(day, month, year);
        }

        // Simple ToString override
        public override string ToString()
        {
            return $"{Day}/{Month}/{Year}";
        }
    }

    

}
