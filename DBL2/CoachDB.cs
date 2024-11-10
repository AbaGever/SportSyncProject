using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models2;
using Org.BouncyCastle.Crypto.Macs;
namespace DBL2
{
    public class CoachDB : BaseDB<Coach>
    {
        protected override string GetTableName()
        {
            return "coaches";
        }
        
        protected override string GetPrimaryKeyName()
        {
            return "id";
        }

        protected override async Task<Coach> CreateModelAsync(object[] row)
        {
            Coach c = new Coach();
            c.id = int.Parse(row[0].ToString());
            c.firstName = row[1].ToString();
            c.lastName = row[2].ToString();
            c.emailaddress = row[3].ToString();
            c.phonenumber = row[4].ToString();
            c.password = row[5].ToString();
            c.sport = row[6].ToString();
            c.groupname = row[7].ToString();
            c.exp = int.Parse(row[8].ToString());
            return c;
        }

        protected override async Task<List<Coach>> CreateListModelAsync(List<object[]> rows)
        {
            List<Coach> coachList = new List<Coach>();
            foreach (object[] item in rows)
            {
                Coach c;
                c = (Coach)await CreateModelAsync(item);
                coachList.Add(c);
            }
            return coachList;
        }

        public async Task<List<Coach>> GetAllAsync()
        {
            return ((List<Coach>)await SelectAllAsync());
        }
        public async Task<bool> insertcoach(Coach c)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
            {"firstName", c.firstName },
            {"lastName", c.lastName},
            {"emailaddress", c.emailaddress },
            {"phonenumber", c.phonenumber },
            {"password", c.password },
            {"sport",c.sport },
            {"groupname",null },
            {"exp", c.exp}
            };
            int num = await base.InsertAsync(data);
            if (num > 0)
            {
                return true;
            }
            else { return false; }
        }




        public async Task<int> UpdateAsync(Coach coach)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                { "id", coach.id.ToString() },
                { "firstName", coach.firstName },
                { "lastName", coach.lastName },
                { "emailaddress" , coach.emailaddress },
                { "phonenumber", coach.phonenumber },
                { "password", coach.password },
                { "sport", coach.sport},
                { "groupname", coach.groupname },               
                { "exp", coach.exp}
            };
            filterValues.Add("id", coach.id.ToString());
            return await base.UpdateAsync(fillValues, filterValues);
        }

        public async Task<int> DeleteAsync(Coach coach)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>
            {
                { "id", coach.id.ToString() }
            };
            return await base.DeleteAsync(filterValues);
        }

        public async Task<Coach> SelectByPkAsync(int id)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("id", id);
            List<Coach> list = (List<Coach>)await SelectAllAsync(p);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }
        

        

        public async Task<Coach> LoginAsync(string email, string password)
        {
            string sql = @"SELECT * FROM sportsync_db.coaches where emailaddress=@emailaddress AND password=@password;";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("emailaddress", email);
            p.Add("password", password);
            List<Coach> list = (List<Coach>)await SelectAllAsync(sql, p);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }
    }
}
