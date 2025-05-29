using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models2
{
    public class Workout // הגדרת מחלקה בשם Workout – מייצגת אימון
    {
        public int id { get; set; } // מזהה ייחודי לאימון
        public int trainerid { get; set; } // מזהה המאמן שאחראי על האימון
        public string date { get; set; } // תאריך קיום האימון בפורמט מחרוזת
        public int duration { get; set; } // משך האימון בדקות
        public string Isgroup { get; set; } // האם האימון קבוצתי – "true" או "false"
        public int hour { get; set; } // שעת תחילת האימון (כמספר)
        public string IsReccuring { get; set; } // האם האימון חוזר (לדוג' כל שבוע) – "true"/"false"

        public Workout() // בנאי ריק – יוצר אובייקט אימון ריק
        {

        }

        public Workout(Workout w) // בנאי שמקבל אובייקט אחר ומעתיק את הערכים שלו
        {
            this.id = w.id; // מעתיק את מזהה האימון
            this.trainerid = w.trainerid; // מעתיק את מזהה המאמן
            this.date = w.date; // מעתיק את התאריך
            this.duration = w.duration; // מעתיק את משך האימון
            this.Isgroup = w.Isgroup; // מעתיק את האם זה אימון קבוצתי
            this.hour = w.hour; // מעתיק את שעת האימון
            this.IsReccuring = w.IsReccuring; // מעתיק את האם האימון חוזר
        }

        public Workout(int id, int trainerid, string date, int duration, string isgroup, int hour, string ir) // בנאי עם פרמטרים – יוצר אימון לפי ערכים
        {
            this.id = id; // מקצה את מזהה האימון
            this.trainerid = trainerid; // מקצה את מזהה המאמן
            this.date = date; // מקצה את התאריך
            this.duration = duration; // מקצה את משך האימון
            this.Isgroup = isgroup; // מקצה האם זה אימון קבוצתי
            this.hour = hour; // מקצה את שעת התחלה
            this.IsReccuring = ir; // מקצה האם האימון חוזר
        }
    }


}
