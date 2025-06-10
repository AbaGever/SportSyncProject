using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Models2;
namespace DBL2
{
    public class TrainerDB : BaseDB<Trainer> // מגדיר מחלקה לניהול טבלת מאמנים היורשת ממחלקת בסיס גנרית
    {
        protected override string GetTableName() // מימוש מתודה אבסטרקטית להחזרת שם הטבלה
        {
            return "trainers"; // מחזיר את שם הטבלה במסד הנתונים
        }

        protected override List<string> GetPrimaryKeyName() // מימוש מתודה אבסטרקטית להחזרת מפתח ראשי
        {
            return new List<string> { "id" }; // מחזיר רשימה עם שם העמודה המהווה מפתח ראשי
        }

        protected override async Task<Trainer> CreateModelAsync(object[] row) // מימוש מתודה ליצירת אובייקט מאמן משורת נתונים
        {
            Trainer c = new Trainer(); // יוצר אובייקט מאמן חדש
            try
            {
                c.id = int.Parse(row[0].ToString()); // ממפה את עמודת ה-ID
                c.firstName = row[1].ToString(); // ממפה את עמודת השם הפרטי
                c.lastName = row[2].ToString(); // ממפה את עמודת השם המשפחה
                c.emailaddress = row[3].ToString(); // ממפה את עמודת האימייל
                c.phonenumber = row[4].ToString(); // ממפה את עמודת הטלפון
                c.password = row[5].ToString(); // ממפה את עמודת הסיסמה
                c.groupname = row[6].ToString(); // ממפה את עמודת שם הקבוצה
                c.isadmin = row[7].ToString(); // ממפה את עמודת הסטטוס אדמין
                c.datejoined = row[8].ToString(); // ממפה את עמודת תאריך ההצטרפות
            }
            catch (Exception ex) // תופס שגיאות במהלך המיפוי
            { c = null; } // מחזיר null במקרה של שגיאה

            return c; // מחזיר את אובייקט המאמן
        }

        protected override async Task<List<Trainer>> CreateListModelAsync(List<object[]> rows) // מימוש מתודה ליצירת רשימת מאמנים
        {
            List<Trainer> userList = new List<Trainer>(); // יוצר רשימה חדשה של מאמנים
            foreach (object[] item in rows) // עובר על כל שורה בתוצאות
            {
                Trainer c;
                c = (Trainer)await CreateModelAsync(item); // יוצר אובייקט מאמן מהשורה
                userList.Add(c); // מוסיף את המאמן לרשימה
            }
            return userList; // מחזיר את הרשימה המלאה
        }

        public async Task<List<Trainer>> GetAllAsync() // מתודה ציבורית לקבלת כל המאמנים
        {
            return ((List<Trainer>)await SelectAllAsync()); // משתמש במתודת הבסיס לאחזור כל הרשומות
        }

        public async Task<bool> insertuser(Trainer user) // מתודה להוספת מאמן חדש
        {
            time t = new time(); // יוצר אובייקט זמן
            Dictionary<string, object> data = new Dictionary<string, object>() // מכין מילון נתונים להכנסה
        {
            {"firstname", user.firstName }, // שם פרטי
            {"lastname", user.lastName}, // שם משפחה
            {"emailaddress", user.emailaddress }, // אימייל
            {"phonenumber", user.phonenumber }, // טלפון
            {"password", user.password }, // סיסמה
            {"groupname",null }, // שם קבוצה (null)
            {"isadmin","false" }, // סטטוס אדמין (כוזב)
            {"datejoined", t.ToString()} // תאריך הצטרפות
        };
            int num = await base.InsertAsync(data); // מבצע את ההכנסה למסד הנתונים
            if (num > 0) // בודק אם ההכנסה הצליחה
            {
                return true; // מחזיר אמת אם הצליח
            }
            else { return false; } // מחזיר שקר אם נכשל
        }

        public async Task<int> UpdateAsync(Trainer customer) // מתודה לעדכון כל פרטי מאמן
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>(); // תנאי סינון לעדכון
            Dictionary<string, object> fillValues = new Dictionary<string, object>() // ערכים חדשים
        {
            { "firstname", customer.firstName }, // עדכון שם פרטי
            { "lastname", customer.lastName }, // עדכון שם משפחה  
            { "emailaddress" , customer.emailaddress }, // עדכון אימייל
            { "phonenumber", customer.phonenumber }, // עדכון טלפון
            { "password", customer.password }, // עדכון סיסמה
            { "groupname", customer.groupname }, // עדכון שם קבוצה
            { "isadmin", customer.isadmin}, // עדכון סטטוס אדמין
            { "datejoined", customer.datejoined} // עדכון תאריך הצטרפות
        };
            filterValues.Add("id", customer.id.ToString()); // סינון לפי ID
            return await base.UpdateAsync(fillValues, filterValues); // מבצע את העדכון
        }

        public async Task<int> UpdateAsyncWithoutGroup(Trainer customer) // מתודה לעדכון מאמן ללא עדכון קבוצה
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
        {
            { "firstname", customer.firstName }, // עדכון שם פרטי
            { "lastname", customer.lastName }, // עדכון שם משפחה
            { "emailaddress" , customer.emailaddress }, // עדכון אימייל
            { "phonenumber", customer.phonenumber }, // עדכון טלפון
            { "password", customer.password }, // עדכון סיסמה
            { "isadmin", customer.isadmin}, // עדכון סטטוס אדמין
            { "datejoined", customer.datejoined} // עדכון תאריך הצטרפות
        };
            filterValues.Add("id", customer.id.ToString()); // סינון לפי ID
            return await base.UpdateAsync(fillValues, filterValues); // מבצע את העדכון
        }

        public async Task<int> DeleteAsync(Trainer customer) // מתודה למחיקת מאמן
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>
        {
            { "id", customer.id.ToString() } // סינון לפי ID
        };
            return await base.DeleteAsync(filterValues); // מבצע את המחיקה
        }

        public async Task<Trainer> SelectByPkAsync(int id) // מתודה לבחירת מאמן לפי מפתח ראשי
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("id", id); // מגדיר פרמטר חיפוש לפי ID
            List<Trainer> list = (List<Trainer>)await SelectAllAsync(p); // מבצע את השאילתה
            if (list.Count == 1) // בודק אם נמצא מאמן אחד
                return list[0]; // מחזיר את המאמן
            else
                return null; // מחזיר null אם לא נמצא
        }

        public async Task<List<(string, string)>> GetNameAndEmail4NonAdminsAsync() // מתודה לקבלת שמות ואימיילים של משתמשים לא אדמינים
        {
            List<(string, string)> returnList = new List<(string, string)>(); // יוצר רשימה לתוצאות
            string sql = "select * from sportsync_db.trainers"; // שאילתת SQL בסיסית
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("isadmin", ""); // פרמטר סינון (ריק)
            List<Trainer> list = (List<Trainer>)await SelectAllAsync(sql, p); // מבצע את השאילתה
            foreach (Trainer item in list) // עובר על התוצאות
            {
                returnList.Add((item.firstName, item.emailaddress)); // מוסיף שם ואימייל לרש�ה
            }
            return returnList; // מחזיר את הרשימה
        }

        public async Task<Trainer> LoginAsync(string email, string password) // מתודה להתחברות משתמש
        {
            string sql = @"SELECT * FROM sportsync_db.trainers where emailaddress=@emailaddress AND password=@password;"; // שאילתת התחברות
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("emailaddress", email); // פרמטר אימייל
            p.Add("password", password); // פרמטר סיסמה
            List<Trainer> list = (List<Trainer>)await SelectAllAsync(sql, p); // מבצע את השאילתה
            if (list.Count == 1) // בודק אם נמצא משתמש אחד
                return list[0]; // מחזיר את המאמן
            else
                return null; // מחזיר null אם לא נמצא
        }

        public async Task<Trainer> EmailCheck(string email) // מתודה לבדיקת קיום אימייל
        {
            string sql = @"SELECT * FROM sportsync_db.trainers where emailaddress=@emailaddress"; // שאילתת בדיקת אימייל
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("emailaddress", email); // פרמטר אימייל
            List<Trainer> list = (List<Trainer>)await SelectAllAsync(sql, p); // מבצע את השאילתה
            if (list.Count == 1) // בודק אם נמצא משתמש אחד
                return list[0]; // מחזיר את המתאמן
            else
                return null; // מחזיר null אם לא נמצא
        }

        public async Task<List<Trainer>> SelectAllInGroup(string groupname) // מתודה לבחירת מאמנים בקבוצה מסוימת
        {
            string sql = @"SELECT * FROM sportsync_db.trainers where groupname = @groupname;"; // שאילתה לפי קבוצה
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("groupname", groupname); // פרמטר שם קבוצה
            List<Trainer> list = (List<Trainer>)await SelectAllAsync(sql, p); // מבצע את השאילתה
            return list; // מחזיר את רשימת המאמנים
        }
    }
}
