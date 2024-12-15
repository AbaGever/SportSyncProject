using DBL2;
using Models2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL2
{
    public class GroupDB : BaseDB<Group>
    {
        protected override string GetTableName()
        {
            return "groups";
        }

        protected override List<string> GetPrimaryKeyName()
        {
            return new List<string> { "name" };
        }

        protected override async Task<Group> CreateModelAsync(object[] row)
        {
            Group g = new Group();
            try
            {
                g.name = row[0].ToString();
                g.maxcapacity = int.Parse(row[1].ToString());
                g.sport = row[2].ToString();
                g.coachid = int.Parse(row[3].ToString());

            }
            catch (Exception ex)
            { g = null; }


            return g;
        }

        protected override async Task<List<Group>> CreateListModelAsync(List<object[]> rows)
        {
            List<Group> userList = new List<Group>();
            foreach (object[] item in rows)
            {
                Group g;
                g = (Group)await CreateModelAsync(item);
                userList.Add(g);
            }
            return userList;
        }

        public async Task<List<Group>> GetAllAsync()
        {
            return ((List<Group>)await SelectAllAsync());
        }
        public async Task<bool> insertgroup(Group group)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
            {"name", group.name },
            {"maxcapacity", group.maxcapacity},
            {"sport", group.sport },
            {"coachid",group.coachid },
            };
            int num = await base.InsertAsync(data);
            if (num > 0)
            {
                return true;
            }
            else { return false; }
        }




        public async Task<int> UpdateAsync(Group g)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                
                { "maxcapacity", g.maxcapacity },
                { "sport", g.sport },
                { "coachid" , g.coachid },

            };
            filterValues.Add("name", g.name.ToString());
            return await base.UpdateAsync(fillValues, filterValues);
        }

        public async Task<int> DeleteAsync(Group g)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>
            {
                { "name", g.name }
            };
            return await base.DeleteAsync(filterValues);
        }

        public async Task<List<Group>> SelectBySportAsync(string sport)
        {
            string q = "SELECT * FROM sportsync_db.groups WHERE sport=@sport;";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("sport", sport);
            List<Group> list = (List<Group>)await SelectAllAsync(q, p);
            return list;
        }
        public async Task<Group> SelectByPkAsync(string name)
        {
            string q = "SELECT * FROM sportsync_db.groups WHERE name=@name;";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("name", name);
            List<Group> list = (List<Group>)await SelectAllAsync(q,p);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }




    }
}
