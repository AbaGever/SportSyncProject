using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using MySql.Data.MySqlClient;
namespace DBL2
{
    public abstract class DB // הגדרת מחלקה אבסטרקטית בשם DB – בסיס להתחברות למסד נתונים
    {
        private const string MySqlConnSTR = @"server=localhost;
                                 user id=root;
                                 password=josh17rog;
                                 persistsecurityinfo=True;
                                 database=sportsync_db";
        // מחרוזת חיבור למסד הנתונים MySQL עם פרטי שרת, משתמש, סיסמה ושם בסיס הנתונים

        protected DbConnection conn; // שדה מוגן שמייצג את חיבור מסד הנתונים
        protected DbCommand cmd; // שדה מוגן שמייצג פקודת SQL לביצוע
        protected DbDataReader reader; // שדה מוגן לקריאת תוצאות מהמסד

        protected DB() // בנאי מוגן של המחלקה האבסטרקטית
        {
            if (conn == null) // אם החיבור למסד עדיין לא קיים
            {
                conn = new MySqlConnection(MySqlConnSTR); // יצירת חיבור חדש עם מחרוזת החיבור
                cmd = new MySqlCommand(); // יצירת פקודת SQL חדשה
            }
            else // אם החיבור כבר קיים
            {
                cmd = new MySqlCommand(); // יצירת פקודת SQL חדשה בלבד
            }
            cmd.Connection = conn; // קישור הפקודה לחיבור למסד הנתונים
            reader = null; // אתחול הקורא לתוצאות לאפס
        }
    }

}
