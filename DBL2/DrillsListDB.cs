using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models2;
using Org.BouncyCastle.Utilities;
namespace DBL2
{
    public class DrillsListDB : BaseDB<drillslist>  // מחלקה שמורשת מבסיס נתונים לטיפול בטבלת drillslist
    {
        protected override string GetTableName()  // הגדרת שם הטבלה במסד הנתונים
        {
            return "drillslist";  // מחזיר את השם של טבלת drillslist
        }

        protected override List<string> GetPrimaryKeyName()  // הגדרת המפתחות הראשיים של הטבלה
        {
            return new List<string> { "Workoutid", "Drillname" };  // מחזיר רשימה עם שמות המפתחות הראשיים
        }

        protected override async Task<drillslist> CreateModelAsync(object[] row)  // יצירת מודל drillslist מתוך שורת נתונים
        {
            drillslist g = new drillslist();  // יצירת מופע חדש של drillslist
            try
            {
                g.Workoutid = int.Parse(row[0].ToString());  // המרת הערך הראשון למספר ושמירתו ב-Workoutid
                g.Drillname = row[1].ToString();  // המרת הערך השני למחרוזת ושמירתו ב-Drillname
            }
            catch (Exception ex)  // טיפול בשגיאות בהמרת הנתונים
            { g = null; }  // במקרה של שגיאה, מחזיר null

            return g;  // מחזיר את האובייקט שנוצר או null במקרה של שגיאה
        }

        protected override async Task<List<drillslist>> CreateListModelAsync(List<object[]> rows)  // יצירת רשימת מודלים מתוך רשימת שורות נתונים
        {
            List<drillslist> drillslist = new List<drillslist>();  // יצירת רשימה חדשה לאחסון המודלים
            foreach (object[] item in rows)  // מעבר על כל שורת נתונים
            {
                drillslist g;  // משתנה לאחסון האובייקט שנוצר
                g = (drillslist)await CreateModelAsync(item);  // יצירת מודל מהשורה הנוכחית
                drillslist.Add(g);  // הוספת המודל לרשימה
            }
            return drillslist;  // החזרת הרשימה של המודלים
        }

        public async Task<bool> InsertDLAsync(drillslist dl)  // הוספת רשומה חדשה לטבלה
        {
            Dictionary<string, object> data = new Dictionary<string, object>()  // יצירת מילון שמכיל את העמודות והערכים להוספה
        {
            {"Workoutid",dl.Workoutid },  // הוספת ערך Workoutid למילון
            {"Drillname" , dl.Drillname},  // הוספת ערך Drillname למילון
        };
            int num = await base.InsertAsync(data);  // קריאה לפונקציה שמכניסה את המידע לטבלה ומחזירה מספר שורות שהושפעו
            if (num > 0)  // בדיקה האם ההוספה הצליחה (יותר מ-0 שורות הושפעו)
            {
                return true;  // החזרת אמת במקרה של הצלחה
            }
            else { return false; }  // החזרת שקר במקרה של כישלון
        }

        public async Task<int> DeleteDLAsync(drillslist dl)  // מחיקת רשומה מהטבלה לפי מפתחות ראשיים
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>  // יצירת מילון עם תנאי הסינון למחיקה
        {
            {"Workoutid",dl.Workoutid },  // הוספת תנאי לפי Workoutid
            {"Drillname", dl.Drillname},  // הוספת תנאי לפי Drillname
        };
            return await base.DeleteAsync(filterValues);  // קריאה למחיקת הרשומה שמקיימת את התנאים, מחזיר את מספר השורות שנמחקו
        }

        public async Task<List<drillslist>> SelectAllInWorkoutAsync(int Workoutid)  // שליפת כל הרשומות בטבלה לפי Workoutid
        {
            string q = "SELECT * FROM sportsync_db.drillslist WHERE Workoutid=@Workoutid;";  // שאילתה לבחירת כל הרשומות לפי Workoutid
            Dictionary<string, object> p = new Dictionary<string, object>();  // יצירת מילון לפרמטרים לשאילתה
            p.Add("Workoutid", Workoutid);  // הוספת הערך של Workoutid לפרמטרים
            List<drillslist> list = await SelectAllAsync(q, p);  // קריאה לשליפה אסינכרונית עם השאילתה והפרמטרים, מחזירה רשימת אובייקטים
            return list;  // החזרת הרשימה שהתקבלה
        }

        public async Task<List<string>> GetDrillsNamesInWorkOut(int Workoutid)  // שליפת שמות התרגילים מתוך רשומות של אימון מסוים
        {
            List<drillslist> result = await SelectAllInWorkoutAsync(Workoutid);  // קריאה לפונקציה שמחזירה את כל הרשומות של האימון

            if (result == null)  // בדיקה אם הרשימה שהתקבלה היא null
            {
                return null;  // החזרת null במידה ולא נמצאו רשומות
            }

            List<string> drillNames = new List<string>();  // יצירת רשימה חדשה לאחסון שמות התרגילים
            foreach (var drill in result)  // מעבר על כל האובייקטים ברשימה
            {
                drillNames.Add(drill.Drillname);  // הוספת שם התרגיל לרשימת השמות
            }

            return drillNames;  // החזרת רשימת שמות התרגילים
        }
    }

}
