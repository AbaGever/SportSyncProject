using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models2;
using Org.BouncyCastle.Crypto.Macs;
namespace DBL2
{
    public class CoachDB : BaseDB<Coach> // מחלקה לניהול טבלת המאמנים, יורשת ממחלקת הבסיס הגנרית
    {
        protected override string GetTableName() // מימוש מתודה אבסטרקטית להחזרת שם הטבלה
        {
            return "coaches"; // מחזיר את שם הטבלה במסד הנתונים
        }

        protected override List<string> GetPrimaryKeyName() // מימוש מתודה אבסטרקטית להחזרת מפתח ראשי
        {
            return new List<string> { "id" }; // מחזיר רשימה עם שם העמודה המהווה מפתח ראשי
        }

        protected override async Task<Coach> CreateModelAsync(object[] row) // מימוש מתודה ליצירת אובייקט מאמן משורת נתונים
        {
            Coach c = new Coach(); // יוצר אובייקט מאמן חדש
            c.id = int.Parse(row[0].ToString()); // ממפה את עמודת ה-ID
            c.firstName = row[1].ToString(); // ממפה את עמודת השם הפרטי
            c.lastName = row[2].ToString(); // ממפה את עמודת השם המשפחה
            c.emailaddress = row[3].ToString(); // ממפה את עמודת האימייל
            c.phonenumber = row[4].ToString(); // ממפה את עמודת הטלפון
            c.password = row[5].ToString(); // ממפה את עמודת הסיסמה
            c.sport = row[6].ToString(); // ממפה את עמודת הספורט
            c.groupname = row[7].ToString(); // ממפה את עמודת שם הקבוצה
            c.exp = int.Parse(row[8].ToString()); // ממפה את עמודת הניסיון
            return c; // מחזיר את אובייקט המאמן
        }

        protected override async Task<List<Coach>> CreateListModelAsync(List<object[]> rows) // מימוש מתודה ליצירת רשימת מאמנים
        {
            List<Coach> coachList = new List<Coach>(); // יוצר רשימה חדשה של מאמנים
            foreach (object[] item in rows) // עובר על כל שורה בתוצאות
            {
                Coach c;
                c = (Coach)await CreateModelAsync(item); // יוצר אובייקט מאמן מהשורה
                coachList.Add(c); // מוסיף את המאמן לרשימה
            }
            return coachList; // מחזיר את הרשימה המלאה
        }

        public async Task<List<Coach>> GetAllAsync() // מתודה ציבורית לקבלת כל המאמנים
        {
            return ((List<Coach>)await SelectAllAsync()); // משתמש במתודת הבסיס לאחזור כל הרשומות
        }

        public async Task<bool> insertcoach(Coach c) // מתודה להוספת מאמן חדש
        {
            Dictionary<string, object> data = new Dictionary<string, object>() // מכין מילון נתונים להכנסה
        {
            {"firstname", c.firstName }, // שם פרטי
            {"lastname", c.lastName}, // שם משפחה
            {"emailaddress", c.emailaddress }, // אימייל
            {"phonenumber", c.phonenumber }, // טלפון
            {"password", c.password }, // סיסמה
            {"sport",c.sport }, // ענף ספורט
            {"groupname",null }, // שם קבוצה (null)
            {"exp", c.exp} // ניסיון
        };
            int num = await base.InsertAsync(data); // מבצע את ההכנסה למסד הנתונים
            if (num > 0) // בודק אם ההכנסה הצליחה
            {
                return true; // מחזיר אמת אם הצליח
            }
            else { return false; } // מחזיר שקר אם נכשל
        }

        public async Task<int> UpdateAsync(Coach coach) // מתודה לעדכון כל פרטי מאמן
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>(); // תנאי סינון לעדכון
            Dictionary<string, object> fillValues = new Dictionary<string, object>() // ערכים חדשים
        {
            { "firstname", coach.firstName }, // עדכון שם פרטי
            { "lastname", coach.lastName }, // עדכון שם משפחה
            { "emailaddress" , coach.emailaddress }, // עדכון אימייל
            { "phonenumber", coach.phonenumber }, // עדכון טלפון
            { "password", coach.password }, // עדכון סיסמה
            { "sport", coach.sport}, // עדכון ענף ספורט
            { "groupname", coach.groupname }, // עדכון שם קבוצה
            { "exp", coach.exp} // עדכון ניסיון
        };
            filterValues.Add("id", coach.id.ToString()); // סינון לפי ID
            return await base.UpdateAsync(fillValues, filterValues); // מבצע את העדכון
        }

        public async Task<int> DeleteAsync(Coach coach) // מתודה למחיקת מאמן
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>
        {
            { "id", coach.id.ToString() } // סינון לפי ID
        };
            return await base.DeleteAsync(filterValues); // מבצע את המחיקה
        }

        public async Task<Coach> SelectByPkAsync(int id) // מתודה לבחירת מאמן לפי מפתח ראשי
        {
            string q = "SELECT * FROM sportsync_db.coaches WHERE id=@id;"; // שאילתת SQL ספציפית
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("id", id); // מגדיר פרמטר חיפוש לפי ID
            List<Coach> list = (List<Coach>)await SelectAllAsync(q, p); // מבצע את השאילתה
            if (list.Count == 1) // בודק אם נמצא מאמן אחד
                return list[0]; // מחזיר את המאמן
            else
                return null; // מחזיר null אם לא נמצא
        }

        public async Task<Coach> EmailCheck(string email) // מתודה לבדיקת קיום אימייל
        {
            string sql = @"SELECT * FROM sportsync_db.coaches where emailaddress=@emailaddress"; // שאילתת בדיקת אימייל
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("emailaddress", email); // פרמטר אימייל
            List<Coach> list = (List<Coach>)await SelectAllAsync(sql, p); // מבצע את השאילתה
            if (list.Count == 1) // בודק אם נמצא משתמש אחד
                return list[0]; // מחזיר את המאמן
            else
                return null; // מחזיר null אם לא נמצא
        }

        public async Task<Coach> LoginAsync(string email, string password) // מתודה להתחברות מאמן
        {
            string sql = @"SELECT * FROM sportsync_db.coaches where emailaddress=@emailaddress AND password=@password;"; // שאילתת התחברות
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("emailaddress", email); // פרמטר אימייל
            p.Add("password", password); // פרמטר סיסמה
            List<Coach> list = (List<Coach>)await SelectAllAsync(sql, p); // מבצע את השאילתה
            if (list.Count == 1) // בודק אם נמצא משתמש אחד
                return list[0]; // מחזיר את המאמן
            else
                return null; // מחזיר null אם לא נמצא
        }

        public async Task<int> UpdateAsyncWithoutGroup(Coach coach) // מתודה לעדכון מאמן ללא עדכון קבוצה
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
        {
            { "firstname", coach.firstName }, // עדכון שם פרטי
            { "lastname", coach.lastName }, // עדכון שם משפחה
            { "emailaddress" , coach.emailaddress }, // עדכון אימייל
            { "phonenumber", coach.phonenumber }, // עדכון טלפון
            { "password", coach.password }, // עדכון סיסמה
            { "sport", coach.sport}, // עדכון ענף ספורט
            { "exp", coach.exp} // עדכון ניסיון
        };
            filterValues.Add("id", coach.id.ToString()); // סינון לפי ID
            return await base.UpdateAsync(fillValues, filterValues); // מבצע את העדכון
        }
    }
}
