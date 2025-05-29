using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Models2
{
    public class drillslist // הגדרת מחלקה בשם drillslist – מייצגת קישור בין תרגיל לאימון
    {
        public int Workoutid { get; set; } // מזהה של האימון שאליו שייך התרגיל
        public string Drillname { get; set; } // שם התרגיל ששייך לאימון

        public drillslist() // בנאי ריק – יוצר אובייקט בלי להכניס ערכים
        {

        }

        public drillslist(int wi, string di) // בנאי עם פרמטרים – יוצר אובייקט עם ערכים
        {
            Workoutid = wi; // שומר את מזהה האימון
            Drillname = di; // שומר את שם התרגיל
        }
    }
}
