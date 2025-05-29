using Models2;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL2
{
    public class WorkoutDB : BaseDB<Workout> // הגדרת מחלקה בשם WorkoutDB שיורשת מהמחלקה BaseDB עבור אובייקטים מסוג Workout
    {
        protected override string GetTableName() // דריסה של פונקציה שמחזירה את שם הטבלה במסד הנתונים
        {
            return "workouts"; // החזרת שם הטבלה: workouts
        }

        protected override List<string> GetPrimaryKeyName() // דריסה של פונקציה שמחזירה את שם המפתח הראשי
        {
            return new List<string> { "id" }; // יצירת רשימה של מפתחות ראשיים, במקרה זה רק "id"
        }

        protected override async Task<Workout> CreateModelAsync(object[] row) // דריסה של פונקציה היוצרת מופע של Workout ממערך של אובייקטים
        {
            Workout w = new Workout(); // יצירת מופע חדש של Workout
            try
            {
                w.id = int.Parse(row[0].ToString()); // המרת הערך הראשון למזהה
                w.trainerid = int.Parse(row[1].ToString()); // המרת הערך השני למזהה מאמן
                w.date = row[2].ToString(); // שמירת התאריך כמחרוזת
                w.duration = int.Parse(row[3].ToString()); // המרת משך האימון למספר
                w.Isgroup = row[4].ToString(); // שמירת האם האימון קבוצתי כמחרוזת
                w.hour = int.Parse(row[5].ToString()); // המרת שעת האימון למספר
                w.IsReccuring = row[6].ToString(); // שמירת האם האימון מחזורי כמחרוזת
            }
            catch (Exception ex)
            { w = null; } // במקרה של שגיאה - החזרת null

            return w; // החזרת מופע ה-Workout
        }

        protected override async Task<List<Workout>> CreateListModelAsync(List<object[]> rows) // יצירת רשימת מופעי Workout מרשימת שורות
        {
            List<Workout> workouts = new List<Workout>(); // יצירת רשימה ריקה של אימונים
            foreach (object[] item in rows) // מעבר על כל שורה
            {
                Workout w; // יצירת מופע חדש
                w = (Workout)await CreateModelAsync(item); // יצירת מופע על פי השורה
                workouts.Add(w); // הוספת האימון לרשימה
            }
            return workouts; // החזרת הרשימה
        }

        public async Task<List<Workout>> GetAllAsync() // החזרת כל האימונים מהמסד
        {
            return ((List<Workout>)await SelectAllAsync()); // ביצוע שאילתת select לכל הטבלה והמרה לרשימת אימונים
        }

        public async Task<bool> InsertWorkout(Workout w) // הוספת אימון חדש למסד
        {
            Dictionary<string, object> data = new Dictionary<string, object>() // יצירת מילון של ערכים להוספה
        {
            {"trainerid",w.trainerid},
            {"date",w.date},
            {"duration",w.duration},
            {"Isgroup",w.Isgroup},
            {"hour",w.hour},
            {"IsReccuring",w.IsReccuring }
        };
            int num = await base.InsertAsync(data); // הוספת הערכים לטבלה באמצעות הפונקציה הבסיסית
            if (num > 0) // בדיקה אם נוסף לפחות שורה אחת
            {
                return true; // החזרה true אם ההוספה הצליחה
            }
            else { return false; } // אחרת, החזרה false
        }

        public async Task<int> UpdateAsync(Workout w) // עדכון אימון קיים במסד
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>(); // מילון לערכים לסינון
            Dictionary<string, object> fillValues = new Dictionary<string, object>() // מילון לערכים לעדכון
        {
            {"trainerid",w.trainerid},
            {"date",w.date},
            {"duration",w.duration},
            {"Isgroup",w.Isgroup},
            {"hour",w.hour},
            {"IsReccuring",w.IsReccuring }
        };

            filterValues.Add("id", w.id); // הוספת המזהה לתנאי הסינון
            return await base.UpdateAsync(fillValues, filterValues); // ביצוע העדכון
        }

        public async Task<int> DeleteAsync(Workout w) // מחיקת אימון מהמסד
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object> // מילון תנאי למחיקה
        {
            { "id", w.id }
        };
            return await base.DeleteAsync(filterValues); // ביצוע המחיקה
        }

        public async Task<bool> IsWorkoutEmpty(int trainerid, string date, int hour) // בדיקה האם יש אימון באותו זמן
        {
            string sql = @$"SELECT * FROM sportsync_db.workouts WHERE trainerid={trainerid} AND date ='{date}' AND hour ={hour};"; // שאילתה לבדוק קיום אימון
            var results = await SelectAllAsync(sql); // שליחת השאילתה למסד
            return !results.Any(); // החזרת true אם אין תוצאות
        }

        public async Task<Workout> SelectByPkAsync(int id) // שליפת אימון לפי מזהה ראשי
        {
            string q = "SELECT * FROM sportsync_db.workouts WHERE id=@id;"; // הגדרת שאילתה עם מזהה
            Dictionary<string, object> p = new Dictionary<string, object>(); // מילון פרמטרים
            p.Add("id", id); // הוספת מזהה
            List<Workout> list = (List<Workout>)await SelectAllAsync(q, p); // שליפת התוצאה
            if (list.Count == 1) // אם יש תוצאה אחת בלבד
                return list[0]; // החזר אותה
            else
                return null; // אחרת החזר null
        }

        public async Task<Workout> InsertGetWorkout(Workout w) // הוספת אימון והחזרת האובייקט שנוסף
        {
            Dictionary<string, object> data = new Dictionary<string, object>() // מילון נתונים
        {
            {"trainerid",w.trainerid},
            {"date",w.date},
            {"duration",w.duration},
            {"Isgroup",w.Isgroup},
            {"hour",w.hour},
            {"IsReccuring",w.IsReccuring }
        };

            Workout workout = (Workout)await base.InsertGetObjAsync(data); // הוספה והחזרת האובייקט החדש
            return workout; // החזרת האימון שנוסף
        }

        public async Task<List<Workout>> GetWorkoutsByTrainerIdAsync(int trainerid) // שליפת כל האימונים לפי מזהה מאמן
        {
            string sql = "SELECT * FROM sportsync_db.workouts WHERE trainerid = @trainerid ORDER BY date, hour;"; // שאילתה לשליפת אימונים לפי מאמן

            Dictionary<string, object> parameters = new Dictionary<string, object> // פרמטרים לשאילתה
        {
            { "@trainerid", trainerid }
        };

            List<Workout> workouts = (List<Workout>)await SelectAllAsync(sql, parameters); // ביצוע השליפה

            return workouts; // החזרת האימונים
        }

        public async Task<List<Workout>> GetWorkoutsByWeekAsync(int trainerid, DateTime startOfWeek) // שליפת אימונים לשבוע לפי מזהה מאמן
        {
            DateTime endOfWeek = startOfWeek.AddDays(6); // חישוב תאריך סוף השבוע

            string startDate = startOfWeek.ToString("yyyy-MM-dd"); // המרת תאריך התחלה לפורמט
            string endDate = endOfWeek.ToString("yyyy-MM-dd"); // המרת תאריך סיום לפורמט

            string sql = @$"SELECT * FROM sportsync_db.workouts 
                WHERE trainerid = @trainerid 
                AND ((date >= @startDate AND date <= @endDate ) 
                OR (IsReccuring = 'true' AND date <= @startDate))
                ORDER BY date, hour;"; // שאילתה לשליפת אימונים שבועיים כולל מחזוריים

            Dictionary<string, object> parameters = new Dictionary<string, object> // פרמטרים לשאילתה
        {
            { "@trainerid", trainerid },
            { "@startDate", startDate },
            { "@endDate", endDate }
        };

            List<Workout> workouts = (List<Workout>)await SelectAllAsync(sql, parameters); // שליפת האימונים מהמסד

            foreach (var workout in workouts) // מעבר על כל האימונים שנשלפו
            {
                if (workout.IsReccuring == "true" && workout.duration > 0) // אם האימון מחזורי
                {
                    DateTime workoutDate = DateTime.Parse(workout.date); // המרת תאריך האימון

                    if (workoutDate < startOfWeek) // אם האימון התרחש לפני תחילת השבוע
                    {
                        int daysToNextOccurrence = ((int)workoutDate.DayOfWeek - (int)startOfWeek.DayOfWeek + 7) % 7; // חישוב מספר הימים עד למועד הבא
                        if (daysToNextOccurrence == 0)
                            daysToNextOccurrence = 7; // אם זה אותו היום בדיוק, דוחים לשבוע הבא

                        workout.date = startOfWeek.AddDays(daysToNextOccurrence).ToString("yyyy-MM-dd"); // עדכון תאריך האימון למועד הקרוב
                    }
                }
            }

            return workouts.OrderBy(w => w.date).ThenBy(w => w.hour).ToList(); // מיון לפי תאריך ושעה והחזרת הרשימה
        }

        public async Task<List<Workout>> GetWorkoutsDailyAsync(int trainerid, DateTime today1) // שליפת אימונים יומיים לפי מאמן ותאריך
        {
            string td = today1.ToString("yyyy-MM-dd"); // המרת תאריך היום לפורמט

            string sql = @$"SELECT * FROM sportsync_db.workouts 
                WHERE trainerid = @trainerid 
                AND (IsReccuring = 'true' AND date <= @td) OR(IsReccuring = 'false' AND date = @td)             
                ORDER BY date, hour;"; // שאילתה לאימונים מחזוריים וחד פעמיים ליום ספציפי

            Dictionary<string, object> parameters = new Dictionary<string, object> // יצירת פרמטרים לשאילתה
        {
            { "@trainerid", trainerid },
            { "@td", td },
        };

            DateTime today = DateTime.ParseExact(td, "yyyy-MM-dd", CultureInfo.InvariantCulture); // המרת מחרוזת תאריך לאובייקט DateTime
            List<Workout> workouts = (List<Workout>)await SelectAllAsync(sql, parameters); // שליפת האימונים
            List<Workout> ws = new List<Workout>(); // רשימת אימונים סופית
            List<Workout> DeletedWorkouts = new List<Workout>(); // רשימת אימונים מבוטלים

            foreach (var workout in workouts) // מעבר על כל האימונים
            {
                if (workout.duration < 0) // בדיקת אימון שבוטל
                {
                    DeletedWorkouts.Add(workout); // הוספה לרשימת המבוטלים
                }
            }

            foreach (var workout in workouts) // מעבר נוסף לבדוק אילו אימונים נשארים
            {
                if (workout.IsReccuring == "true" && workout.duration > 0) // אימון מחזורי
                {
                    DateTime workoutDate = DateTime.Parse(workout.date); // המרת תאריך

                    if (workoutDate < today) // בדיקה אם האימון התרחש בעבר
                    {
                        DayOfWeek WorkoutDOW = workoutDate.DayOfWeek;
                        DayOfWeek todayDOW = today.DayOfWeek;

                        if (WorkoutDOW == todayDOW) // בדיקה אם אותו יום בשבוע
                        {
                            int count = 0;
                            foreach (Workout wd in DeletedWorkouts) // בדיקה אם האימון בוטל
                            {
                                if (wd.duration == workout.id * -1 && DateTime.Parse(wd.date) == DateTime.Parse(workout.date))
                                {
                                    count++;
                                }
                            }
                            if (count == 0 && workout.duration > 0) // אם לא בוטל
                            {
                                workout.date = today.ToString("yyyy-MM-dd"); // עדכון תאריך
                                ws.Add(workout); // הוספה לרשימה הסופית
                            }
                        }
                    }
                }
                else if (workout.IsReccuring == "false" && workout.duration > 0) // אימון לא מחזורי
                {
                    int count = 0;
                    foreach (Workout wd in DeletedWorkouts) // בדיקה אם בוטל
                    {
                        if (wd.duration == workout.id * -1 && DateTime.Parse(wd.date) == DateTime.Parse(workout.date))
                        {
                            count++;
                        }
                    }
                    if (count == 0 && workout.duration > 0) // אם לא בוטל
                    {
                        ws.Add(workout); // הוספה לרשימה הסופית
                    }
                }
            }

            return ws.OrderBy(w => w.date).ThenBy(w => w.hour).ToList(); // מיון לפי תאריך ושעה והחזרת הרשימה
        }
    }

}


