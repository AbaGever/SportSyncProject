using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models2
{
    public class Group // הגדרת מחלקה בשם Group
    {
        public string name { get; set; } // שם הקבוצה
        public int maxcapacity { get; set; } // מקסימום משתתפים בקבוצה
        public string sport { get; set; } // סוג הספורט של הקבוצה
        public int coachid { get; set; } // מזהה המאמן שאחראי על הקבוצה

        public Group() // בנאי ריק – יוצר קבוצה בלי פרטים
        {

        }

        public Group(string n, int mx, string s, int ci) // בנאי עם פרמטרים: שם, קיבולת, ספורט, מזהה מאמן
        {
            name = n; // הגדרת שם הקבוצה
            maxcapacity = mx; // הגדרת מקסימום משתתפים
            sport = s; // הגדרת סוג הספורט
            coachid = ci; // הגדרת מזהה המאמן
        }
    }

}
