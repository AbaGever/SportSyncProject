using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models2
{
    public class Coach
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailaddress { get; set; }
        public string phonenumber { get; set; }
        public string password { get; set; }
        public string sport { get; set; }
        public string groupname { get; set; }
        public int exp { get; set; }

        public Coach()
        {

        }
        public Coach(int Id, string Fname, string Lname, string email, string number, string ps, string sprt, string gname, int exp1)
        {
            id = Id;
            firstName = Fname;
            lastName = Lname;
            emailaddress = email;
            phonenumber = number;
            password = ps;
            sport = sprt;
            groupname = gname;
            exp = exp1;
        }

    }

}
