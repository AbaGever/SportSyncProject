using Models2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL2
{
    public class WorkoutDB : BaseDB<Workout>
    {

        protected override string GetTableName()
        {
            return "workouts";
        }

        protected override List<string> GetPrimaryKeyName()
        {
            return new List<string> { "id" };
        }

        protected override async Task<Workout> CreateModelAsync(object[] row)
        {
            Workout w = new Workout();
            try
            {
                w.id = int.Parse(row[0].ToString());
                w.trainerid = int.Parse(row[1].ToString());
                w.date = row[2].ToString();
                w.duration = int.Parse(row[3].ToString());
                w.Isgroup = row[4].ToString();
                w.hour = int.Parse(row[5].ToString());
                w.IsReccuring = row[6].ToString();

            }
            catch (Exception ex)
            { w = null; }


            return w;
        }

        protected override async Task<List<Workout>> CreateListModelAsync(List<object[]> rows)
        {
            List<Workout> workouts = new List<Workout>();
            foreach (object[] item in rows)
            {
                Workout w;
                w = (Workout)await CreateModelAsync(item);
                workouts.Add(w);
            }
            return workouts;
        }

        public async Task<List<Workout>> GetAllAsync()
        {
            return ((List<Workout>)await SelectAllAsync());
        }
        public async Task<bool> InsertWorkout(Workout w)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {

            {"trainerid",w.trainerid},
            {"date",w.date},
            {"duration",w.duration},
            {"Isgroup",w.Isgroup},
            {"hour",w.hour},
            {"IsReccuring",w.IsReccuring }
            };
            int num = await base.InsertAsync(data);
            if (num > 0)
            {
                return true;
            }
            else { return false; }
        }




        public async Task<int> UpdateAsync(Workout w)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
            {"trainerid",w.trainerid},
            {"date",w.date},
            {"duration",w.duration},
            {"Isgroup",w.Isgroup},
            {"hour",w.hour},
            {"IsReccuring",w.IsReccuring }
            };


            filterValues.Add("id", w.id);
            return await base.UpdateAsync(fillValues, filterValues);
        }

        public async Task<int> DeleteAsync(Workout w)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>
            {
                { "id", w.id }
            };
            return await base.DeleteAsync(filterValues);
        }

        public async Task<bool> IsWorkoutEmpty(int trainerid, string date, int hour)
        {
            string sql = @$"SELECT * FROM sportsync_db.workouts WHERE trainerid={trainerid} AND date ='{date}' AND hour =@hour;";
            var results = await SelectAllAsync(sql);
            return !results.Any();
        }
        public async Task<Workout> SelectByPkAsync(int id)
        {
            string q = "SELECT * FROM sportsync_db.workouts WHERE id=@id;";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("id", id);
            List<Workout> list = (List<Workout>)await SelectAllAsync(q, p);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }

        public async Task<Workout> InsertGetWorkout(Workout w)
        {
            if (await IsWorkoutEmpty(w.trainerid, w.date, w.hour))
            {
                Dictionary<string, object> data = new Dictionary<string, object>()
            {
            {"trainerid",w.trainerid},
            {"date",w.date},
            {"duration",w.duration},
            {"Isgroup",w.Isgroup},
            {"hour",w.hour},
            {"IsReccuring",w.IsReccuring }
            };
                Workout workout = (Workout)await base.InsertGetObjAsync(data);
                return workout;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Workout>> GetWorkoutsByTrainerIdAsync(int trainerid)
        {
            // בניית השאילתה SQL כדי לקבל את כל האימונים של המתאמן
            string sql = "SELECT * FROM sportsync_db.workouts WHERE trainerid = @trainerid ORDER BY date, hour;";

            // יצירת פרמטרים לשאילתה
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@trainerid", trainerid }
            };

            // שליחת השאילתה לקבלת האימונים
            List<Workout> workouts = (List<Workout>)await SelectAllAsync(sql, parameters);

            return workouts;
        }

        public async Task<List<Workout>> GetWorkoutsByWeekAsync(int trainerid, DateTime startOfWeek)
        {
            // חישוב סוף השבוע - אנחנו מניחים שיום ראשון הוא תחילת השבוע
            DateTime endOfWeek = startOfWeek.AddDays(6);

            // המרת התאריכים לפורמט שמתאים לשאילתה
            string startDate = startOfWeek.ToString("yyyy-MM-dd");
            string endDate = endOfWeek.ToString("yyyy-MM-dd");

            // בניית שאילתה SQL כדי לקבל את האימונים בטווח התאריכים
            string sql = @$"
                           SELECT * FROM sportsync_db.workouts
                           WHERE trainerid = @trainerid
                           AND ((date >= @startDate AND date <= @endDate ) OR (IsReccuring = 'true') AND date <= @startDate)
                           ORDER BY date, hour
                            ";

            // יצירת פרמטרים לשאילתה
            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "@trainerid", trainerid },
        { "@startDate", startDate },
        { "@endDate", endDate }
    };

            // שליחת השאילתה לקבלת האימונים
            List<Workout> workouts = (List<Workout>)await SelectAllAsync(sql, parameters);

            // יצירת רשימה חדשה לאימונים שתכיל גם את האימונים המחזוריים
            List<Workout> allWorkouts = new List<Workout>(workouts);

            // הוספת אימונים מחזוריים לכל יום תואם בשבוע
            foreach (var workout in workouts)
            {
                if (workout.IsReccuring == "true") // אם האימון מחזורי
                {
                    // המרת תאריך האימון ל- DateTime
                    DateTime workoutDate = DateTime.Parse(workout.date);
                    DayOfWeek recurringDay = workoutDate.DayOfWeek;

                    // הוספת האימון לכל יום מתאים בשבוע
                    foreach (var day in Enumerable.Range(0, 7)) // נעבור על כל הימים בשבוע
                    {
                        DateTime newDate = startOfWeek.AddDays(day);
                        if (newDate.DayOfWeek == recurringDay)
                        {
                            // יצירת אימון מחזורי שמתאים ליום הזה
                            Workout recurringWorkout = new Workout
                            {
                                trainerid = workout.trainerid,
                                date = newDate.ToString("yyyy-MM-dd"),
                                duration = workout.duration,
                                Isgroup = workout.Isgroup,
                                hour = workout.hour,
                                IsReccuring = workout.IsReccuring
                            };

                            // אם האימון לא קיים כבר ברשימה, נוסיף אותו
                            if (!allWorkouts.Any(w => w.date == recurringWorkout.date && w.hour == recurringWorkout.hour))
                            {
                                allWorkouts.Add(recurringWorkout);
                            }
                        }
                    }
                }
            }

            // מחזירים את כל האימונים, כולל מחזוריים, רק על בסיס הצגת תוצאות ולא שמירה במסד הנתונים
            return allWorkouts.OrderBy(w => w.date).ThenBy(w => w.hour).ToList();

        }
    }
}


