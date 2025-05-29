using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models2
{
    public class TrainerNew // הגדרת מחלקה חדשה בשם TrainerNew
    {
        public TrainerNew() // בנאי ריק – יוצר אובייקט בלי להכניס ערכים
        {

        }

        public TrainerNew(string email, string pass) // בנאי עם שני פרמטרים: אימייל וסיסמה
        {
            this.email = email; // מגדיר את שדה האימייל של האובייקט
            this.pass = pass; // מגדיר את שדה הסיסמה של האובייקט
        }

        public string email { get; set; } // שדה לאחסון כתובת האימייל

        public string pass { get; set; } // שדה לאחסון הסיסמה
    }
}
