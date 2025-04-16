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
            string sql = @$"SELECT * FROM sportsync_db.workouts WHERE trainerid={trainerid} AND date ='{date}' AND hour ={hour};";
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

        public async Task<List<Workout>> GetWorkoutsDailyAsync(int trainerid, DateTime today)
        {
            

            string td = today.ToString("yyyy-MM-dd");

            string sql = @$"SELECT * FROM sportsync_db.workouts 
                    WHERE trainerid = @trainerid 
                    AND (date = @today) 
                    ORDER BY date, hour;";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
        { "@trainerid", trainerid },
        { "@startDate", today },
        { "@endDate", endDate }
             };

        //    List<Workout> workouts = (List<Workout>)await SelectAllAsync(sql, parameters);

        //    foreach (var workout in workouts)
        //    {
        //        if (workout.IsReccuring == "true" && workout.duration > 0)
        //        {
        //            DateTime workoutDate = DateTime.Parse(workout.date);

        //            // אם תאריך האימון קטן מה- startDate, נמצא את התאריך הבא שמתאים לאותו יום בשבוע אחרי startDate
        //            if (workoutDate < startOfWeek)
        //            {
        //                int daysToNextOccurrence = ((int)workoutDate.DayOfWeek - (int)startOfWeek.DayOfWeek + 7) % 7;
        //                if (daysToNextOccurrence == 0)
        //                    daysToNextOccurrence = 7; // אם זה אותו היום, נעביר לשבוע הבא

        //                workout.date = startOfWeek.AddDays(daysToNextOccurrence).ToString("yyyy-MM-dd");
        //            }
        //        }
        //    }

        //    return workouts.OrderBy(w => w.date).ThenBy(w => w.hour).ToList();
        //}

        public async Task<List<Workout>> GetWorkoutsDailyAsync(int trainerid, DateTime today)
        {


            string sql = @$"SELECT * FROM sportsync_db.workouts 
                    WHERE trainerid = @trainerid 
                    AND (IsReccuring = 'true' AND date <= @today)               
                    ORDER BY date, hour;";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
        { "@trainerid", trainerid },
        { "@today", today },
             };

            List<Workout> workouts = (List<Workout>)await SelectAllAsync(sql, parameters);
            List<Workout> ws = new List<Workout>();
            foreach (var workout in workouts)
            {
                if (workout.IsReccuring == "true" && workout.duration > 0)
                {
                    DateTime workoutDate = DateTime.Parse(workout.date);

                    // אם תאריך האימון קטן מה- startDate, נמצא את התאריך הבא שמתאים לאותו יום בשבוע אחרי startDate
                    if (workoutDate < today)
                    {
                        DayOfWeek WorkoutDOW = workoutDate.DayOfWeek;
                        DayOfWeek todayDOW = today.DayOfWeek;

                        if (WorkoutDOW == todayDOW)
                        {
                            workout.date = today.ToString("yyyy-MM-dd");
                            ws.Add(workout);
                        }
                    }
                }
            }

            return ws.OrderBy(w => w.date).ThenBy(w => w.hour).ToList();
        }
    }
}


