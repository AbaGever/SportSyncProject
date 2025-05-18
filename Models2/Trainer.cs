using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models2
{
    public class Trainer
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailaddress { get; set; }
        public string phonenumber { get; set; }
        public string password { get; set; }
        public string groupname { get; set; } 
        public string isadmin { get; set; } 
        public string datejoined 
        {
            get; set;
        
        }

        public Trainer()
        {
            isadmin = "false";
            time t = new time();
            datejoined = t.ToString();


        }
        public Trainer(int Id, string Fname, string Lname, string email, string number, string ps, string gname, string isAdmin, string datej)
        {
            id = Id;
            firstName = Fname;
            lastName = Lname;
            emailaddress = email;
            password = ps;
            phonenumber = number;
            isadmin = isAdmin;
            groupname = gname;
            datejoined = datej;
        }
         public Trainer( string Fname, string Lname, string email, string number, string ps, string gname, string isAdmin, string datej)
        {
            
            firstName = Fname;
            lastName = Lname;
            emailaddress = email;
            password = ps;
            phonenumber = number;
            isadmin = isAdmin;
            groupname = gname;
            datejoined = datej;
        }
    }
}
