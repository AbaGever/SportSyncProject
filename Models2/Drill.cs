using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models2
{
    public class Drill // הגדרת מחלקה בשם Drill – תרגיל
    {
        public string name { get; set; } // שם התרגיל
        public string muscle { get; set; } // קבוצת השרירים שעליהם עובד התרגיל
        public int sets { get; set; } // כמות סטים בתרגיל
        public int reps { get; set; } // כמות חזרות בכל סט
        public int duration { get; set; } // משך התרגיל בשניות או דקות (תלוי מה החלטתם)
        public string description { get; set; } // תיאור מילולי של התרגיל

        public Drill() // בנאי ריק – יוצר תרגיל בלי ערכים
        {

        }

        public Drill(string n, string m, int s, int r, int dur, string des) // בנאי עם כל הפרטים של התרגיל
        {
            name = n; // הגדרת שם התרגיל
            muscle = m; // הגדרת קבוצת שרירים
            sets = s; // הגדרת כמות סטים
            reps = r; // הגדרת כמות חזרות
            duration = dur; // הגדרת משך התרגיל
            description = des; // הגדרת תיאור של התרגיל
        }
    }
}
