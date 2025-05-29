using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models2
{
    using System;

    public class time // הגדרת מחלקה בשם time – מייצגת תאריך פשוט
    {
        public int Day { get; set; } // שדה עבור היום
        public int Month { get; set; } // שדה עבור החודש
        public int Year { get; set; } // שדה עבור השנה

        // Constructor with parameters
        public time(int day, int month, int year) // בנאי עם פרמטרים – יוצר תאריך עם ערכים מסוימים
        {
            this.Day = day; // קובע את ערך היום
            this.Month = month; // קובע את ערך החודש
            this.Year = year; // קובע את ערך השנה
        }

        // Default constructor sets to the current date
        public time() // בנאי ברירת מחדל – מגדיר את התאריך להיום
        {
            var currentDate = DateTime.Now; // לוקח את התאריך והשעה הנוכחיים
            this.Day = currentDate.Day; // שומר את היום הנוכחי
            this.Month = currentDate.Month; // שומר את החודש הנוכחי
            this.Year = currentDate.Year; // שומר את השנה הנוכחית
        }

        // Parse method without using Split or parts
        public static time Parse(string dateStr) // מתודה סטטית שמקבלת מחרוזת תאריך ומחזירה אובייקט time
        {
            int day = int.Parse(dateStr.Substring(0, 2)); // שולף את היום מתוך המחרוזת (2 תווים ראשונים)
            int month = int.Parse(dateStr.Substring(3, 2)); // שולף את החודש מתוך המחרוזת (2 תווים באמצע)
            int year = int.Parse(dateStr.Substring(6, 4)); // שולף את השנה מתוך המחרוזת (4 תווים אחרונים)

            return new time(day, month, year); // יוצר ומחזיר אובייקט חדש של time לפי הערכים
        }

        // Simple ToString override
        public override string ToString() // מתודה שמחזירה מחרוזת תיאור של התאריך
        {
            return $"{Year}-{Month}-{Day}"; // מחזירה את התאריך בפורמט: שנה-חודש-יום
        }
    }



}
