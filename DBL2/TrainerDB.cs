using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Models2;
namespace DBL2
{
    public class TrainerDB : BaseDB<Trainer>
    {
        protected override string GetTableName()
        {
            return "trainers";
        }

        protected override string GetPrimaryKeyName()
        {
            return "id";
        }

        protected override async Task<Trainer> CreateModelAsync(object[] row)
        {
            Trainer c = new Trainer();
            try {
                c.id = int.Parse(row[0].ToString());
                c.firstName = row[1].ToString();
                c.lastName = row[2].ToString();
                c.emailaddress = row[3].ToString();
                c.phonenumber = row[4].ToString();
                c.password = row[5].ToString();
                c.groupname = row[6].ToString();
                c.isadmin = row[7].ToString();
                c.datejoined = row[8].ToString();
            }
            catch (Exception ex)
            { c = null; }
            
            
            return c;
        }

        protected override async Task<List<Trainer>> CreateListModelAsync(List<object[]> rows)
        {
            List<Trainer> userList = new List<Trainer>();
            foreach (object[] item in rows)
            {
                Trainer c;
                c = (Trainer)await CreateModelAsync(item);
                userList.Add(c);
            }
            return userList;
        }

        public async Task<List<Trainer>> GetAllAsync()
        {
            return ((List<Trainer>)await SelectAllAsync());
        }
        public async Task<bool> insertuser(Trainer user)
        {
            time t = new time();
            Dictionary<string, object> data = new Dictionary<string, object>()
            { 
            {"firstname", user.firstName },
            {"lastname", user.lastName},
            {"emailaddress", user.emailaddress },
            {"phonenumber", user.phonenumber },
            {"password", user.password },
            {"groupname",null },
            {"isadmin","false" },
            {"datejoined", t.ToString()}
            };
            int num = await base.InsertAsync(data);
            if (num > 0)
            {
                return true;
            }
            else { return false; }
        }


       

        public async Task<int> UpdateAsync(Trainer customer)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                { "firstname", customer.firstName },
                { "lastname", customer.lastName },  
                { "emailaddress" , customer.emailaddress },
                { "phonenumber", customer.phonenumber },
                { "password", customer.password },
                { "groupname", customer.groupname },
                { "isadmin", customer.isadmin},
                { "datejoined", customer.datejoined}
            };
            filterValues.Add("id", customer.id.ToString());
            return await base.UpdateAsync(fillValues, filterValues);
        }
        public async Task<int> UpdateAsyncWithoutGroup(Trainer customer)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                { "firstname", customer.firstName },
                { "lastname", customer.lastName },
                { "emailaddress" , customer.emailaddress },
                { "phonenumber", customer.phonenumber },
                { "password", customer.password },
                { "isadmin", customer.isadmin},
                { "datejoined", customer.datejoined}
            };
            filterValues.Add("id", customer.id.ToString());
            return await base.UpdateAsync(fillValues, filterValues);
        }

        public async Task<int> DeleteAsync(Trainer customer)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>
            {
                { "id", customer.id.ToString() }
            };
            return await base.DeleteAsync(filterValues);
        }

        public async Task<Trainer> SelectByPkAsync(int id)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("id", id);
            List<Trainer> list = (List<Trainer>)await SelectAllAsync(p);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }


        public async Task<List<(string, string)>> GetNameAndEmail4NonAdminsAsync()
        {
            List<(string, string)> returnList = new List<(string, string)>();
            string sql = "select * from sportsync_db.trainers";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("isadmin", "");
            List<Trainer> list = (List<Trainer>)await SelectAllAsync(sql, p);
            foreach (Trainer item in list)
            {
                returnList.Add((item.firstName, item.emailaddress));
            }
            return returnList;
        }

        public async Task<Trainer> LoginAsync(string email, string password)
        {
            string sql = @"SELECT * FROM sportsync_db.trainers where emailaddress=@emailaddress AND password=@password;";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("emailaddress", email);
            p.Add("password", password);
            List<Trainer> list = (List<Trainer>)await SelectAllAsync(sql, p);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }

        public async Task<List<Trainer>> SelectAllInGroup(string groupname)
        {
            string sql = @"SELECT * FROM sportsync_db.trainers where groupname = @groupname;";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("groupname", groupname);
            List<Trainer> list = (List<Trainer>)await SelectAllAsync(sql, p);
            return list;


        }
    }
}
