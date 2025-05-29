using DBL2;
using Models2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL2
{
    public class GroupDB : BaseDB<Group>
    // מגדיר מחלקה GroupDB שיורשת מ-BaseDB עבור טיפוס Group

    {
        protected override string GetTableName()
        // מיישם פונקציה שמחזירה את שם הטבלה במסד הנתונים עבור האובייקט Group

        {
            return "groups";
            // מחזיר את המחרוזת "groups" כשם הטבלה במסד
        }

        protected override List<string> GetPrimaryKeyName()
        // מיישם פונקציה שמחזירה רשימה של שמות העמודות שמרכיבות את המפתח הראשי

        {
            return new List<string> { "name" };
            // מחזיר רשימה עם מחרוזת אחת: "name" כמפתח ראשי
        }

        protected override async Task<Group> CreateModelAsync(object[] row)
        // מיישם פונקציה אסינכרונית ליצירת מופע Group מתוך מערך אובייקטים (שורה ממסד)

        {
            Group g = new Group();
            // יוצר מופע חדש של Group

            try
            {
                g.name = row[0].ToString();
                // ממיר את הערך במיקום 0 למחרוזת ומשייך לשדה name

                g.maxcapacity = int.Parse(row[1].ToString());
                // ממיר את הערך במיקום 1 למחרוזת, מפענח למספר שלם ומשייך ל-maxcapacity

                g.sport = row[2].ToString();
                // ממיר את הערך במיקום 2 למחרוזת ומשייך ל-sport

                g.coachid = int.Parse(row[3].ToString());
                // ממיר את הערך במיקום 3 למחרוזת, מפענח למספר שלם ומשייך ל-coachid

            }
            catch (Exception ex)
            { g = null; }
            // במקרה של שגיאה, מאפס את המשתנה g ל-null

            return g;
            // מחזיר את האובייקט Group שנוצר או null במקרה של שגיאה
        }

        protected override async Task<List<Group>> CreateListModelAsync(List<object[]> rows)
        // מיישם פונקציה אסינכרונית ליצירת רשימה של Groups מתוך רשימת שורות (מערך אובייקטים)

        {
            List<Group> userList = new List<Group>();
            // יוצר רשימה ריקה של Group

            foreach (object[] item in rows)
            {
                Group g;
                // מגדיר משתנה Group

                g = (Group)await CreateModelAsync(item);
                // יוצר Group מכל שורה באופן אסינכרוני

                userList.Add(g);
                // מוסיף את האובייקט שנוצר לרשימה
            }
            return userList;
            // מחזיר את רשימת ה-Groups שנוצרו
        }

        public async Task<List<Group>> GetAllAsync()
        // פונקציה ציבורית אסינכרונית לקבלת כל ה-Groups מהמסד

        {
            return ((List<Group>)await SelectAllAsync());
            // קוראת ל-SelectAllAsync מהמחלקה הבסיסית ומחזירה את הרשימה
        }

        public async Task<bool> insertgroup(Group group)
        // פונקציה ציבורית אסינכרונית להוספת Group חדש למסד

        {
            Dictionary<string, object> data = new Dictionary<string, object>()  
        // יוצרת מילון מפתחות-ערכים לנתוני ה-Group להוספה

        {
            {"name", group.name },  
            // מפתח "name" וערך מתוך group.name

            {"maxcapacity", group.maxcapacity},  
            // מפתח "maxcapacity" וערך מתוך group.maxcapacity

            {"sport", group.sport },  
            // מפתח "sport" וערך מתוך group.sport

            {"coachid",group.coachid },  
            // מפתח "coachid" וערך מתוך group.coachid
        };
            int num = await base.InsertAsync(data);
            // קורא לפונקציה אסינכרונית InsertAsync במחלקה הבסיסית ומקבל מספר רשומות שהוכנסו

            if (num > 0)
            // אם הוכנס לפחות רשומה אחת

            {
                return true;
                // מחזיר אמת - ההוספה הצליחה
            }
            else { return false; }
            // אחרת מחזיר שקר - ההוספה נכשלה
        }

        public async Task<int> UpdateAsync(Group g)
        // פונקציה ציבורית אסינכרונית לעדכון רשומת Group קיימת במסד

        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            // יוצרת מילון לסינון הרשומות לעדכון (WHERE)

            Dictionary<string, object> fillValues = new Dictionary<string, object>()  
        // יוצרת מילון של הערכים החדשים לעדכון (SET)

        {
            { "maxcapacity", g.maxcapacity },  
            // מגדירה את הערך החדש ל-maxcapacity

            { "sport", g.sport },  
            // מגדירה את הערך החדש ל-sport

            { "coachid" , g.coachid },  
            // מגדירה את הערך החדש ל-coachid
        };
            filterValues.Add("name", g.name.ToString());
            // מוסיפה לסינון את שם ה-Group לעדכון לפי השדה name

            return await base.UpdateAsync(fillValues, filterValues);
            // קוראת לפונקציה אסינכרונית לעדכון ומחזירה את מספר הרשומות שהושפעו
        }

        public async Task<int> DeleteAsync(Group g)
        // פונקציה ציבורית אסינכרונית למחיקת רשומת Group מהמסד

        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>  
        // יוצרת מילון סינון למחיקה

        {
            { "name", g.name }  
            // סינון לפי שדה name של ה-Group למחיקה
        };
            return await base.DeleteAsync(filterValues);
            // קוראת למחיקה אסינכרונית ומחזירה את מספר הרשומות שנמחקו
        }

        public async Task<List<Group>> SelectBySportAsync(string sport)
        // פונקציה ציבורית אסינכרונית לשליפת רשימת Groups לפי שדה sport

        {
            string q = "SELECT * FROM sportsync_db.groups WHERE sport=@sport;";
            // מגדירה את שאילתת ה-SQL עם פרמטר לספורט

            Dictionary<string, object> p = new Dictionary<string, object>();
            // יוצרת מילון לפרמטרים

            p.Add("sport", sport);
            // מוסיפה את פרמטר הספורט למילון

            List<Group> list = (List<Group>)await SelectAllAsync(q, p);
            // קוראת לשאילתת SQL אסינכרונית עם הפרמטרים וממירה לרשימת Group

            return list;
            // מחזירה את הרשימה שנשלפה
        }

        public async Task<Group> SelectByPkAsync(string name)
        // פונקציה ציבורית אסינכרונית לשליפת Group יחיד לפי המפתח הראשי (name)

        {
            string q = "SELECT * FROM sportsync_db.groups WHERE name=@name;";
            // מגדירה שאילתת SQL עם פרמטר שם

            Dictionary<string, object> p = new Dictionary<string, object>();
            // יוצרת מילון לפרמטרים

            p.Add("name", name);
            // מוסיפה את פרמטר השם למילון

            List<Group> list = (List<Group>)await SelectAllAsync(q, p);
            // קוראת לשאילתת SQL אסינכרונית עם הפרמטרים וממירה לרשימת Group

            if (list.Count == 1)
                // אם הרשימה מכילה פריט אחד בלבד

                return list[0];
            // מחזירה את הפריט הראשון

            else
                return null;
            // אחרת מחזירה null (לא נמצא או יותר מפריט אחד)
        }

    }

}
