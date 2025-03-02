using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models2
{
    public class TrainerNew
    {
        public TrainerNew()
        {

        }

        public TrainerNew(string email, string pass)
        {
            this.email = email;
            this.pass = pass;
        }

        public string email { get; set; }

        public string pass { get; set; }

    }
}
