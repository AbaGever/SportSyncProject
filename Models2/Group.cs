using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models2
{
    public class Group
    {
        public string name { get; set; }
        public int maxcapacity { get; set; }
        public string sport { get; set; }
        public int coachid { get; set; }

        public Group() 
        { 
        
        }
        public Group(string n, int mx,string s,int ci) 
        {
            name = n;
            maxcapacity = mx;
            sport = s;
            coachid = ci;
        }
        
    }
    
}
