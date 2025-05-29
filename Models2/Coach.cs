using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models2
{
    public class Coach // הגדרת מחלקה חדשה בשם Coach
    {
        public int id { get; set; } // מזהה ייחודי למאמן
        public string firstName { get; set; } // שם פרטי של המאמן
        public string lastName { get; set; } // שם משפחה של המאמן
        public string emailaddress { get; set; } // כתובת אימייל של המאמן
        public string phonenumber { get; set; } // מספר טלפון של המאמן
        public string password { get; set; } // סיסמה של המאמן
        public string sport { get; set; } // סוג הספורט שהמאמן מתמחה בו
        public string groupname { get; set; } // שם הקבוצה של המאמן
        public int exp { get; set; } // מספר שנות ניסיון של המאמן

        public Coach() // בנאי ריק – יוצר מאמן בלי להגדיר ערכים
        {

        }

        public Coach(int Id, string Fname, string Lname, string email, string number, string ps, string sprt, int exp1)
        {
            id = Id; // מגדיר את המזהה
            firstName = Fname; // מגדיר את השם הפרטי
            lastName = Lname; // מגדיר את שם המשפחה
            emailaddress = email; // מגדיר את כתובת האימייל
            phonenumber = number; // מגדיר את מספר הטלפון
            password = ps; // מגדיר את הסיסמה
            sport = sprt; // מגדיר את סוג הספורט
            groupname = ""; // מגדיר שם קבוצה ריק
            exp = exp1; // מגדיר את שנות הניסיון
        }

        public Coach(int Id, string Fname, string Lname, string email, string number, string ps, string sprt, string gname, int exp1)
        {
            id = Id; // מגדיר את המזהה
            firstName = Fname; // מגדיר את השם הפרטי
            lastName = Lname; // מגדיר את שם המשפחה
            emailaddress = email; // מגדיר את כתובת האימייל
            phonenumber = number; // מגדיר את מספר הטלפון
            password = ps; // מגדיר את הסיסמה
            sport = sprt; // מגדיר את סוג הספורט
            groupname = gname; // מגדיר את שם הקבוצה
            exp = exp1; // מגדיר את שנות הניסיון
        }
    }

}
