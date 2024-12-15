using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Models2
{
    public class drillslist
    {
        public int Workoutid { get; set; }
        public string Drillname { get; set; }

        public drillslist()
        {

        }
        public drillslist(int wi,string di)
        {
            Workoutid = wi;
            Drillname = di;
        }
    }
}
