using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models2
{
    public class Drill
    {
        public string name { get; set; }
        public string muscle { get; set; }
        public int sets { get; set; }
        public int reps { get; set; }
        public int duration { get; set; }
        public string description { get; set; }

        public Drill()
        {

        }
        public Drill(string n, string m, int s, int r,int dur,string des)
        {
            name = n;
            muscle = m;    
             sets = s;
            reps = r;
            duration = dur;
            description = des;
        }
    }
}
