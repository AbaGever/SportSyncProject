using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DBL2
{

    public abstract class BaseDB<T> : DB // מגדיר מחלקה אבסטרקטית גנרית עם פרמטר טיפוס T היורשת ממחלקה בשם DB
    {
        protected abstract string GetTableName(); // מגדיר מתודה אבסטרקטית להחזרת שם הטבלה במסד הנתונים
        protected abstract List<string> GetPrimaryKeyName(); // מגדיר מתודה אבסטרקטית להחזרת שמות המפתחות הראשיים
        protected abstract Task<T> CreateModelAsync(object[] row); // מגדיר מתודה אבסטרקטית ליצירת מודל מטיפוס T משורת נתונים
        protected abstract Task<List<T>> CreateListModelAsync(List<object[]> rows); // מגדיר מתודה אבסטרקטית ליצירת רשימת מודלים מרשימת שורות

        /// <summary>
        /// A generic operation to retrieve ALL data from the database.
        /// </summary>
        protected async Task<List<T>> SelectAllAsync() // מגדיר מתודה אסינכרונית לאחזור כל הנתונים ללא פרמטרים
        {
            return await SelectAllAsync("", new Dictionary<string, object>()); // קורא לגרסה המלאה של המתודה עם פרמטרי ברירת מחדל
        }

        /// <summary>
        /// A generic operation to retrieve data from the database.
        /// </summary>
        protected async Task<List<T>> SelectAllAsync(Dictionary<string, object> parameters) // מגדיר מתודה אסינכרונית לאחזור נתונים עם פרמטרי חיפוש
        {
            return await SelectAllAsync("", parameters); // קורא לגרסה המלאה עם שאילתת ברירת מחדל
        }

        /// <summary>
        /// A generic operation to retrieve data from the database.
        /// </summary>
        protected async Task<List<T>> SelectAllAsync(string query) // מגדיר מתודה אסינכרונית לאחזור נתונים עם שאילתה מותאמת
        {
            return await SelectAllAsync(query, new Dictionary<string, object>()); // קורא לגרסה המלאה עם פרמטרי ברירת מחדל
        }

        /// <summary>
        /// A generic operation to retrieve data from the database.
        /// </summary>
        protected async Task<List<T>> SelectAllAsync(string query, Dictionary<string, object> parameters) // מגדיר מתודה אסינכרונית לאחזור נתונים עם שאילתה ופרמטרים
        {
            List<object[]> list = await StingListSelectAllAsync(query, parameters); // מבצע את השאילתה ומקבל רשימת שורות גולמיות
            return await CreateListModelAsync(list); // ממיר את הרשימה הגולמית לרשימת מודלים
        }

        /// <summary>
        /// Insert new records in a table using INSERT Statement.
        /// </summary>
        protected async Task<int> InsertAsync(Dictionary<string, object> keyAndValue) // מגדיר מתודה אסינכרונית להכנסת רשומה חדשה
        {
            string sqlCommand = PrepareInsertQueryWithParameters(keyAndValue); // מכין את פקודת ה-INSERT עם פרמטרים
            return await ExecNonQueryAsync(sqlCommand); // מבצע את הפקודה ומחזיר מספר שורות שהושפעו
        }

        /// <summary>
        /// Insert new records in a table using INSERT Statement.
        /// </summary>
        protected async Task<object> InsertGetObjAsync(Dictionary<string, object> keyAndValue) // מגדיר מתודה להכנסת רשומה וקבלת האובייקט שהוכנס
        {
            string sqlCommand = PrepareInsertQueryWithParameters(keyAndValue); // מכין את פקודת ה-INSERT הבסיסית
            sqlCommand += $" SELECT LAST_INSERT_ID();"; // מוסיף פקודה לקבלת המפתח הראשי שהוקצה
            object res = await ExecScalarAsync(sqlCommand); // מבצע את השאילתה ומקבל את המפתח
            if (res != null) // בודק אם התקבל מפתח חוקי
            {
                Dictionary<string, object> p = new Dictionary<string, object>(); // יוצר מילון פרמטרים לחיפוש
                p.Add("id", res.ToString()); // מוסיף את המפתח שהתקבל כפרמטר
                string sql; // מכין שאילתת SELECT לאחזור הרשומה שהוכנסה
                if (GetPrimaryKeyName().Count == 1) // בודק אם יש מפתח ראשי בודד
                    sql = @$"SELECT * FROM {GetTableName()} WHERE ({GetPrimaryKeyName()[0]} = @id)"; // בונה שאילתה למפתח בודד
                else
                    sql = @$"SELECT * FROM {GetTableName()} WHERE ({GetPrimaryKeyName()[0]},{GetPrimaryKeyName()[1]} = @id)"; // בונה שאילתה למפתח מרובה
                List<T> list = (List<T>)await SelectAllAsync(sql, p); // מבצע את השאילתה ומקבל רשימה
                if (list.Count == 1) // בודק אם התקבלה רשומה אחת
                    return list[0]; // מחזיר את האובייקט שהוכנס
                else
                    return null; // מחזיר null אם לא נמצאה רשומה
            }
            else
                return null; // מחזיר null אם לא התקבל מפתח
        }

        /// <summary>
        /// Update records in a table using SQL UPDATE Statement.
        /// </summary>
        protected async Task<int> UpdateAsync(Dictionary<string, object> FildValue, Dictionary<string, object> parameters) // מגדיר מתודה לעדכון רשומות
        {
            string where = PrepareWhereQueryWithParameters(parameters); // מכין את תנאי ה-WHERE
            string InKeyValue = PrepareUpdateQueryWithParameters(FildValue); // מכין את חלק הערכים ב-SET
            if (string.IsNullOrEmpty(InKeyValue)) // בודק אם אין ערכים לעדכון
                return 0; // מחזיר 0 אם אין מה לעדכון
            string sqlCommand = $"UPDATE sportsync_db.{GetTableName()} SET {InKeyValue}  {where}"; // בונה את פקודת העדכון המלאה
            return await ExecNonQueryAsync(sqlCommand); // מבצע את העדכון ומחזיר מספר שורות שהושפעו
        }

        /// <summary>
        /// Delete records in a table using SQL DELETE Statement.
        /// </summary>
        protected async Task<int> DeleteAsync(Dictionary<string, object> parameters) // מגדיר מתודה למחיקת רשומות
        {
            string where = PrepareWhereQueryWithParameters(parameters); // מכין את תנאי ה-WHERE
            string sqlCommand = $"DELETE FROM sportsync_db.{GetTableName()} {where}"; // בונה את פקודת המחיקה
            return await ExecNonQueryAsync(sqlCommand); // מבצע את המחיקה ומחזיר מספר שורות שהושפעו
        }

        /// <summary>
        /// Add one parameters to SQL statement.
        /// </summary>
        private void AddParameterToCommand(string name, object value) // מתודה להוספת פרמטר לפקודת SQL
        {
            DbParameter p = cmd.CreateParameter(); // יוצר אובייקט פרמטר חדש
            p.ParameterName = name; // קובע את שם הפרמטר
            p.Value = value; // קובע את ערך הפרמטר
            cmd.Parameters.Add(p); // מוסיף את הפרמטר לאוסף הפרמטרים
        }

        /// <summary>
        /// Prepare command and Connection before executing SQL command
        /// </summary>
        private async Task PreQueryAsync(string query) // מתודה להכנת הפקודה והחיבור לפני ביצוע
        {
            cmd.CommandText = query; // קובע את טקסט הפקודה
            if (conn.State != System.Data.ConnectionState.Open) // בודק אם החיבור סגור
            {
                await conn.OpenAsync(); // פותח את החיבור אם סגור
            }
            if (cmd.Connection.State != System.Data.ConnectionState.Open) // בודק אם הפקודה לא מחוברת
            {
                cmd.Connection = conn; // מחבר את הפקודה לחיבור
            }
        }

        /// <summary>
        /// Make cleanup after sql command was executed
        /// </summary>
        private async Task PostQueryAsync() // מתודה לניקוי משאבים לאחר ביצוע פקודה
        {
            if (reader != null && !reader.IsClosed) // בודק אם קורא הנתונים פתוח
                await reader.CloseAsync(); // סוגר את קורא הנתונים

            cmd.Parameters.Clear(); // מנקה את אוסף הפרמטרים
            if (conn.State == System.Data.ConnectionState.Open) // בודק אם החיבור פתוח
                await conn.CloseAsync(); // סוגר את החיבור
        }

        /// <summary>
        /// Prepare a WHERE SQL Query, from given paremeters dictionary
        /// </summary>
        private string PrepareWhereQueryWithParameters(Dictionary<string, object> parameters) // מתודה להכנת תנאי WHERE ממילון פרמטרים
        {
            string where = "WHERE "; // התחלת בניית תנאי WHERE
            string AND = "AND"; // מחרוזת לוגית
            if (parameters != null && parameters.Count > 0) // בודק אם קיימים פרמטרים
            {
                foreach (KeyValuePair<string, object> param in parameters) // עובר על כל הפרמטרים
                {
                    string prm = $"@W{param.Key}"; // יוצר שם פרמטר ייחודי
                    where += $"{param.Key}={prm} {AND} "; // מוסיף תנאי השוואה
                    AddParameterToCommand(prm, param.Value); // מוסיף את הפרמטר לפקודה
                }
                where = where.Remove(where.Length - AND.Length - 2); // מסיר את ה-AND המיותר האחרון
            }
            else
                where = ""; // מחזיר מחרוזת ריקה אם אין פרמטרים
            return where; // מחזיר את מחרוזת התנאי
        }

        /// <summary>
        /// Prepare Update Query With Parameters
        /// </summary>
        private string PrepareUpdateQueryWithParameters(Dictionary<string, object> fields) // מתודה להכנת חלק ה-SET בעדכון
        {
            string InValue = ""; // מחרוזת לאחסון ערכים
            if (fields != null && fields.Count > 0) // בודק אם קיימים שדות לעדכון
            {
                foreach (KeyValuePair<string, object> param in fields) // עובר על כל השדות
                {
                    string prm = $"@{param.Key}"; // יוצר שם פרמטר
                    InValue += $"{param.Key}={prm},"; // מוסיף זוג שדה=ערך
                    AddParameterToCommand(prm, param.Value); // מוסיף פרמטר לפקודה
                }
                InValue = InValue.Remove(InValue.Length - 1); // מסיר פסיק מיותר
            }
            return InValue; // מחזיר את מחרוזת הערכים
        }

        /// <summary>
        /// Prepare Insert Query With Parameters
        /// </summary>
        private string PrepareInsertQueryWithParameters(Dictionary<string, object> fields) // מתודה להכנת פקודת INSERT
        {
            if (fields == null || fields.Count == 0) // בודק אם אין שדות להכנסה
                return ""; // מחזיר מחרוזת ריקה

            string InKey = "(" + string.Join(",", fields.Keys) + ")"; // יוצר רשימת שמות עמודות
            string InValue = "VALUES("; // התחלת בניית רשימת ערכים
            for (int i = 0; i < fields.Values.Count; i++) // עובר על הערכים
            {
                string pn = "@" + i; // יוצר שם פרמטר אוטומטי
                InValue += pn + ','; // מוסיף את הפרמטר לרשימה
                AddParameterToCommand(pn, fields.Values.ElementAt(i)); // מוסיף את הפרמטר לפקודה
            }
            InValue = InValue.Remove(InValue.Length - 1); // מסיר פסיק מיותר
            InValue += ")"; // סוגר את רשימת הערכים

            string sqlCommand = $"INSERT INTO sportsync_db.{GetTableName()}  {InKey} {InValue};"; // בונה את הפקודה המלאה
            return sqlCommand; // מחזיר את פקודת ה-INSERT
        }

        /// <summary>
        /// Executes the command against its connection object, returning the number of rows affected. 
        /// </summary>
        private async Task<int> ExecNonQueryAsync(string query) // מתודה לביצוע פקודות שאינן שאילתות (INSERT/UPDATE/DELETE)
        {
            if (String.IsNullOrEmpty(query)) // בודק אם הפקודה ריקה
                return 0; // מחזיר 0 אם ריקה
            await PreQueryAsync(query); // מכין את הפקודה והחיבור
            int rowsEffected = 0; // מאתחל מונה שורות
            try
            {
                rowsEffected = await cmd.ExecuteNonQueryAsync(); // מבצע את הפקודה
            }
            catch (Exception e) // תופס שגיאות
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nsql:" + cmd.CommandText); // רושם שגיאה
            }
            finally
            {
                await PostQueryAsync(); // מבצע ניקוי
            }
            return rowsEffected; // מחזיר את מספר השורות שהושפעו
        }

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the result
        /// </summary>
        private async Task<object> ExecScalarAsync(string query) // מתודה לביצוע שאילתה והחזרת ערך בודד
        {
            if (String.IsNullOrEmpty(query)) // בודק אם הפקודה ריקה
                return null; // מחזיר null אם ריקה
            await PreQueryAsync(query); // מכין את הפקודה והחיבור
            object obj = null; // מאתחל ערך החזרה
            try
            {
                obj = await cmd.ExecuteScalarAsync(); // מבצע את השאילתה
            }
            catch (Exception e) // תופס שגיאות
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nsql:" + cmd.CommandText); // רושם שגיאה
            }
            finally
            {
                await PostQueryAsync(); // מבצע ניקוי
            }
            return obj; // מחזיר את התוצאה
        }

        /// <summary>
        /// Prepare the main WHERE SQL Query, from given paremeters dictionary
        /// </summary>
        private async Task<List<object[]>> StingListSelectAllAsync(string query, Dictionary<string, object> parameters) // מתודה לביצוע שאילתות SELECT
        {
            List<object[]> list = new List<object[]>(); // יוצר רשימה לאחסון תוצאות
            string sqlCommand = ""; // מאתחל את פקודת ה-SQL
            if (String.IsNullOrEmpty(query)) // בודק אם לא סופקה שאילתה מותאמת
            {
                string where = PrepareWhereQueryWithParameters(parameters); // מכין תנאי WHERE
                sqlCommand = $"SELECT * FROM {GetTableName()} {where}"; // בונה שאילתת ברירת מחדל
            }
            else if (query.IndexOf("@") > 0) // בודק אם השאילתה מכילה פרמטרים
            {
                sqlCommand = query; // משתמש בשאילתה הנתונה
                foreach (KeyValuePair<string, object> param in parameters) // מוסיף פרמטרים לפקודה
                {
                    AddParameterToCommand($"@{param.Key}", param.Value);
                }
            }
            else // אם השאילתה לא מכילה פרמטרים
            {
                string where = PrepareWhereQueryWithParameters(parameters); // מכין תנאי WHERE
                sqlCommand = $"{query} {where}"; // משרשר את תנאי ה-WHERE לשאילתה
            }
            await PreQueryAsync(sqlCommand); // מכין את הפקודה והחיבור
            try
            {
                reader = await cmd.ExecuteReaderAsync(); // מבצע את השאילתה ומקבל DataReader
                var readOnlyData = await reader.GetColumnSchemaAsync(); // מקבל מידע על העמודות
                int size = readOnlyData.Count; // קובע את מספר העמודות
                object[] row; // מערך לאחסון שורה
                while (reader.Read()) // קורא שורות תוצאה
                {
                    row = new object[size]; // יוצר מערך חדש
                    reader.GetValues(row); // ממלא את המערך בערכי השורה
                    list.Add(row); // מוסיף את השורה לרשימה
                }
            }
            catch (Exception e) // תופס שגיאות
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nsql:" + cmd.CommandText); // רושם שגיאה
                list.Clear(); // מנקה את הרשימה
            }
            finally
            {
                await PostQueryAsync(); // מבצע ניקוי
            }
            return list; // מחזיר את רשימת השורות
        }
    }
}