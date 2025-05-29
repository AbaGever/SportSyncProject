using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models2
{
    public class Trainer // הגדרת מחלקה חדשה בשם Trainer
    {
        public int id { get; set; } // מזהה ייחודי למאמן
        public string firstName { get; set; } // שם פרטי של המאמן
        public string lastName { get; set; } // שם משפחה של המאמן
        public string emailaddress { get; set; } // כתובת אימייל של המאמן
        public string phonenumber { get; set; } // מספר טלפון של המאמן
        public string password { get; set; } // סיסמה של המאמן
        public string groupname { get; set; } // שם הקבוצה של המאמן
        public string isadmin { get; set; } // האם המאמן מוגדר כאדמין (true/false)
        public string datejoined // תאריך הצטרפות של המאמן
        {
            get; set;
        }

        public Trainer() // בנאי ריק – יוצר מאמן בלי להכניס פרטים
        {
            isadmin = "false"; // ברירת מחדל – המאמן לא אדמין
            time t = new time(); // יצירת אובייקט מסוג time
            datejoined = t.ToString(); // המרת התאריך למחרוזת ושמירתו ב-datejoined
        }

        public Trainer(int Id, string Fname, string Lname, string email, string number, string ps, string gname, string isAdmin, string datej)
        {
            id = Id; // הגדרת מזהה
            firstName = Fname; // הגדרת שם פרטי
            lastName = Lname; // הגדרת שם משפחה
            emailaddress = email; // הגדרת כתובת אימייל
            password = ps; // הגדרת סיסמה
            phonenumber = number; // הגדרת מספר טלפון
            isadmin = isAdmin; // הגדרת אם המאמן אדמין
            groupname = gname; // הגדרת שם קבוצה
            datejoined = datej; // הגדרת תאריך הצטרפות
        }

        public Trainer(string Fname, string Lname, string email, string number, string ps, string gname, string isAdmin, string datej)
        {
            firstName = Fname; // הגדרת שם פרטי
            lastName = Lname; // הגדרת שם משפחה
            emailaddress = email; // הגדרת כתובת אימייל
            password = ps; // הגדרת סיסמה
            phonenumber = number; // הגדרת מספר טלפון
            isadmin = isAdmin; // הגדרת אם המאמן אדמין
            groupname = gname; // הגדרת שם קבוצה
            datejoined = datej; // הגדרת תאריך הצטרפות
        }
    }
}
