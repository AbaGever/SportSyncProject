using Microsoft.AspNetCore.Mvc;

using DBL2;
using Models2;
using System;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportSyncAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    // הגדרת קונטרולר בשם LoginController שיורש מהמחלקה ControllerBase לצורך יצירת API

    {
        [HttpGet("{trainerid:int},{d:int},{m:int},{y:int}")]
        // מגדיר שהפעולה הזו תגיב ל-HTTP GET עם פרמטרים מספריים ב-URL: מזהה מאמן, יום, חודש ושנה

        [ActionName("GetDailyW")]
        // נותן שם לפעולה לצורך זיהוי ברוטינג או תיעוד

        public async Task<List<Workout>> GetDW(int trainerid, int d, int m, int y)
        // פעולה אסינכרונית שמחזירה רשימת אימונים לפי מזהה מאמן ותאריך

        {
            DateTime tm = new DateTime(y, m, d);
            // יוצר אובייקט DateTime מתאריך שהתקבל כפרמטרים (שנה, חודש, יום)

            string td = tm.ToString("yyyy-MM-dd");
            // ממיר את התאריך למחרוזת בפורמט yyyy-MM-dd

            DateTime t = DateTime.ParseExact(td, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            // ממיר חזרה את המחרוזת לאובייקט DateTime לפי פורמט מדויק ותרבות אינvariant

            WorkoutDB wdb = new WorkoutDB();
            // יוצר מופע חדש של מחלקת הגישה למסד נתונים של אימונים

            List<Workout> workouts = await wdb.GetWorkoutsDailyAsync(trainerid, t);
            // שולף את רשימת האימונים מהמסד עבור מאמן מסוים ותאריך מסוים באופן אסינכרוני

            return workouts;
            // מחזיר את רשימת האימונים
        }

        [HttpPost]
        // מציין שהפעולה תגיב לבקשות HTTP POST

        [ActionName("Register")]
        // נותן שם לפעולה – Register – לזיהוי חיצוני

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // מציין שבמקרה של שגיאה פנימית הפעולה תחזיר סטטוס 500

        public async Task<ActionResult<int>> Post1([FromBody] Trainer item)
        // פעולה אסינכרונית שמקבלת אובייקט מאמן מהגוף של הבקשה ומחזירה מזהה של המאמן החדש

        {
            TrainerDB db = new TrainerDB();
            // יוצר מופע חדש של מחלקת הגישה למסד נתונים של מאמנים

            bool b;
            // מגדיר משתנה בוליאני

            b = await db.insertuser(item);
            // מנסה להכניס את המשתמש למסד נתונים באופן אסינכרוני ושומר את תוצאת ההצלחה

            if (b)
            {
                return item.id;
                // אם ההכנסה הצליחה, מחזיר את המזהה של המאמן החדש
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK);
                // אם ההכנסה נכשלה, מחזיר קוד 200 למרות זאת (לא סטנדרטי)
            }
        }

        [HttpPost]
        // פעולה נוספת שמגיבה לבקשת POST

        [ActionName("Log")]
        // נותן שם לפעולה – Log – לזיהוי חיצוני

        public async Task<ActionResult<Trainer>> Post2([FromBody] TrainerNew item)
        // פעולה אסינכרונית שמקבלת אובייקט כניסה של מאמן ומחזירה את המאמן אם הצליח להתחבר

        {
            TrainerDB UserDB = new TrainerDB();
            // יוצר מופע חדש של מחלקת הגישה למסד נתונים של מאמנים

            Trainer User = await UserDB.LoginAsync(item.email, item.pass);
            // מבצע ניסיון התחברות עם אימייל וסיסמה, ושומר את תוצאת ההתחברות

            if (User == null)
            {
                return BadRequest("User not found");
                // אם המאמן לא נמצא, מחזיר תשובת שגיאה 400 עם הודעה
            }

            return Ok(User);
            // אם המאמן נמצא, מחזיר את פרטי המאמן עם סטטוס 200
        }

        [HttpPut]
        // פעולה שמגיבה לבקשת HTTP PUT

        [ActionName("Ch")]
        // נותן שם לפעולה – Ch – לזיהוי חיצוני

        public async Task<ActionResult<Trainer>> Change([FromBody] Trainer t)
        // פעולה אסינכרונית שמקבלת אובייקט מאמן ומבצעת עדכון בפרטי המאמן במסד נתונים

        {
            TrainerDB UserDB = new TrainerDB();
            // יוצר מופע חדש של מחלקת הגישה למסד נתונים של מאמנים

            int n = await UserDB.UpdateAsyncWithoutGroup(t);
            // מנסה לעדכן את פרטי המאמן במסד נתונים בלי לשנות את הקבוצה

            if (n <= 0)
            {
                return BadRequest("Couldnt Update User");
                // אם לא עודכן אף שורה, מחזיר שגיאה 400 עם הודעה
            }

            return StatusCode(StatusCodes.Status200OK);
            // אם העדכון הצליח, מחזיר קוד 200
        }
    }

}
