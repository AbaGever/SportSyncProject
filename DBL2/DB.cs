using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using MySql.Data.MySqlClient;
namespace DBL2
{
    public abstract class DB
    {

        private const string MySqlConnSTR = @"server=localhost;
                                    user id=root;
                                    password=josh17rog;
                                    persistsecurityinfo=True;
                                    database=sportsync_db";

        protected DbConnection conn;
        protected DbCommand cmd;
        protected DbDataReader reader;

        protected DB()
        {
            if (conn == null)
            {

                conn = new MySqlConnection(MySqlConnSTR);
                cmd = new MySqlCommand();

            }
            else
            {

                cmd = new MySqlCommand();

            }
            cmd.Connection = conn;
            reader = null;
        }
    }
}
