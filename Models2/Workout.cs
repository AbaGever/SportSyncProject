using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models2
{
    public class Workout
    {
        public int id {  get; set; }
        public int trainerid { get; set; }
        public string date { get; set; }
        public int duration { get; set; }
        public string Isgroup { get; set; }
        public int hour { get; set; }
        public string IsReccuring { get; set; }

        public Workout()
        {

        }
        public Workout(int id, int trainerid, string date, int duration, string isgroup, int hour,string ir)
        {
            this.id = id;
            this.trainerid = trainerid;
            this.date = date;
            this.duration = duration;
            this.Isgroup = isgroup;
            this.hour = hour;
            this.IsReccuring = ir; 
        }
    }

}
