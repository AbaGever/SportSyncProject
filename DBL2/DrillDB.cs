using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models2;
using Org.BouncyCastle.Crypto.Macs;
namespace DBL2
{
    public class DrillDB : BaseDB<Drill>  // מגדיר מחלקה DrillDB שיורשת מ-BaseDB עם טיפוס Drill
    {
        protected override string GetTableName()  // פונקציה שמחזירה את שם הטבלה במסד הנתונים
        {
            return "drills";  // מחזירה את שם הטבלה "drills"
        }

        protected override List<string> GetPrimaryKeyName()  // פונקציה שמחזירה רשימת שמות מפתחות ראשיים בטבלה
        {
            return new List<string> { "name" };  // מחזירה רשימה עם מפתח ראשי יחיד "name"
        }

        protected override async Task<Drill> CreateModelAsync(object[] row)  // יוצרת מופע Drill מתוך שורה של אובייקטים מהטבלה
        {
            Drill d = new Drill();  // מגדירה משתנה חדש מסוג Drill
            d.name = row[0].ToString();  // ממירה את הערך בעמודה 0 למחרוזת ומאחסנת בשדה name
            d.muscle = row[1].ToString();  // ממירה את הערך בעמודה 1 למחרוזת ומאחסנת בשדה muscle
            d.sets = int.Parse(row[2].ToString());  // ממירה את הערך בעמודה 2 למספר שלם ומאחסנת בשדה sets
            d.reps = int.Parse(row[3].ToString());  // ממירה את הערך בעמודה 3 למספר שלם ומאחסנת בשדה reps
            d.duration = int.Parse(row[4].ToString());  // ממירה את הערך בעמודה 4 למספר שלם ומאחסנת בשדה duration
            d.description = row[5].ToString();  // ממירה את הערך בעמודה 5 למחרוזת ומאחסנת בשדה description
            return d;  // מחזירה את מופע ה-Drill שנוצר
        }

        protected override async Task<List<Drill>> CreateListModelAsync(List<object[]> rows)  // יוצרת רשימה של מופעי Drill מתוך רשימת שורות אובייקטים
        {
            List<Drill> drills = new List<Drill>();  // מגדירה רשימה ריקה מסוג Drill
            foreach (object[] item in rows)  // עוברת על כל שורה ברשימת השורות
            {
                Drill d;  // מגדירה משתנה Drill
                d = (Drill)await CreateModelAsync(item);  // יוצרת Drill מתוך השורה הנוכחית ומחכה לתוצאה
                drills.Add(d);  // מוסיפה את Drill לרשימה
            }
            return drills;  // מחזירה את רשימת ה-Drill שנוצרו
        }

        public async Task<List<Drill>> GetAllAsync()  // מחזירה רשימה של כל ה-Drill מהטבלה
        {
            return ((List<Drill>)await SelectAllAsync());  // קוראת לפונקציה בסיסית SelectAllAsync ומחזירה את התוצאה כמפורט Drill
        }
        public async Task<bool> insertdrill(Drill d)  // מכניסה Drill חדש לטבלה ומחזירה האם ההכנסה הצליחה
        {
            Dictionary<string, object> data = new Dictionary<string, object>()  // יוצרת מילון נתונים להוספה בטבלה
        {
        {"name", d.name },  // מוסיפה זוג מפתח-ערך לשדה name
        {"muscle", d.muscle},  // מוסיפה זוג מפתח-ערך לשדה muscle
        {"sets", d.sets },  // מוסיפה זוג מפתח-ערך לשדה sets
        {"reps", d.reps },  // מוסיפה זוג מפתח-ערך לשדה reps
        {"duration", d.duration},  // מוסיפה זוג מפתח-ערך לשדה duration
        {"description",d.description },  // מוסיפה זוג מפתח-ערך לשדה description
        };
            int num = await base.InsertAsync(data);  // קוראת לפונקציה InsertAsync בבסיס ומחזירה את מספר השורות שהושפעו
            if (num > 0)  // בודקת אם נוספה לפחות שורה אחת
            {
                return true;  // מחזירה true אם ההכנסה הצליחה
            }
            else { return false; }  // מחזירה false אם ההכנסה נכשלה
        }

        public async Task<int> UpdateAsync(Drill d)  // מעדכנת רשומה קיימת לפי מפתח ראשי ומחזירה מספר רשומות שהשתנו
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();  // יוצרת מילון לסינון הרשומות לעדכון
            Dictionary<string, object> fillValues = new Dictionary<string, object>()  // מילון לערכי העדכון עצמם
        {
            {"name", d.name },  // מוסיפה זוג מפתח-ערך לעדכון שדה name
            {"muscle", d.muscle},  // מוסיפה זוג מפתח-ערך לעדכון שדה muscle
            {"sets", d.sets },  // מוסיפה זוג מפתח-ערך לעדכון שדה sets
            {"reps", d.reps },  // מוסיפה זוג מפתח-ערך לעדכון שדה reps
            {"duration", d.duration},  // מוסיפה זוג מפתח-ערך לעדכון שדה duration
            {"description",d.description },  // מוסיפה זוג מפתח-ערך לעדכון שדה description
        };
            filterValues.Add("name", d.name);  // מוסיפה את מפתח הסינון לפי name
            return await base.UpdateAsync(fillValues, filterValues);  // קוראת לפונקציה UpdateAsync בבסיס עם הערכים והסינון ומחזירה את התוצאה
        }

        public async Task<int> DeleteAsync(Drill d)  // מוחקת רשומה לפי מפתח ראשי ומחזירה מספר רשומות שנמחקו
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>  // יוצרת מילון לסינון המחיקה
        {
            { "name", d.name }  // מוסיפה את מפתח הסינון לפי name
        };
            return await base.DeleteAsync(filterValues);  // קוראת לפונקציה DeleteAsync בבסיס עם הסינון ומחזירה את התוצאה
        }

        public async Task<Drill> SelectByPkAsync(string name)  // בוחרת רשומה לפי מפתח ראשי ומחזירה מופע Drill או null
        {
            Dictionary<string, object> p = new Dictionary<string, object>();  // יוצרת מילון לפרמטרים של השאילתה
            p.Add("name", name);  // מוסיפה את המפתח הראשי name למילון הפרמטרים
            List<Drill> list = (List<Drill>)await SelectAllAsync(p);  // קוראת לפונקציה SelectAllAsync עם הפרמטרים ומקבלת רשימה
            if (list.Count == 1)  // בודקת אם נמצאה בדיוק רשומה אחת
                return list[0];  // מחזירה את הרשומה
            else
                return null;  // מחזירה null אם לא נמצאה רשומה אחת
        }
        public async Task<List<Drill>> SelectByMuscleAsync(string muscle)  // מחזירה רשימת Drill לפי ערך muscle ספציפי
        {
            string sql = @"SELECT * FROM sportsync_db.drills where muscle = @muscle;";  // מגדירה שאילתת SQL לבחירת רשומות לפי muscle
            Dictionary<string, object> p = new Dictionary<string, object>();  // יוצרת מילון לפרמטרים של השאילתה
            p.Add("muscle", muscle);  // מוסיפה את הערך muscle למילון הפרמטרים
            List<Drill> list = (List<Drill>)await SelectAllAsync(sql, p);  // קוראת לפונקציה SelectAllAsync עם השאילתה והפרמטרים ומחזירה רשימה
            return list;  // מחזירה את הרשימה
        }

        public async Task<List<Drill>> SelectByMuscleOrNameAsync(string name, string muscle)  // מחזירה רשימת Drill לפי התאמה ל-name או muscle באמצעות ביטוי רגולרי
        {
            string sql = @"SELECT * FROM drills WHERE name RLIKE @name OR muscle RLIKE @muscle;";  // מגדירה שאילתת SQL עם תנאי RLIKE על name ו-muscle
            Dictionary<string, object> p = new Dictionary<string, object>();  // יוצרת מילון לפרמטרים של השאילתה
            if (name == "")  // בודקת אם name ריק
                name = "_";  // מגדירה שם ברירת מחדל של "_"
            if (muscle == "")  // בודקת אם muscle ריק
                muscle = "_";  // מגדירה muscle ברירת מחדל של "_"
            p.Add("@name", name);  // מוסיפה את הפרמטר name למילון עם המפתח "@name"
            p.Add("@muscle", muscle);  // מוסיפה את הפרמטר muscle למילון עם המפתח "@muscle"
            List<Drill> list = (List<Drill>)await SelectAllAsync(sql, p);  // קוראת לפונקציה SelectAllAsync עם השאילתה והפרמטרים ומחזירה רשימה
            return list;  // מחזירה את הרשימה
        }
    }

}

