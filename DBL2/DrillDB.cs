using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models2;
using Org.BouncyCastle.Crypto.Macs;
namespace DBL2
{
    public class DrillDB : BaseDB<Drill>
    {
        protected override string GetTableName()
        {
            return "drills";
        }

        protected override string GetPrimaryKeyName()
        {
            return "name";
        }

        protected override async Task<Drill> CreateModelAsync(object[] row)
        {
            Drill d = new Drill();
            d.name = row[0].ToString();
            d.muscle = row[1].ToString();
            d.sets = int.Parse(row[2].ToString());
            d.reps = int.Parse(row[3].ToString());
            d.duration = int.Parse(row[4].ToString());
            d.description = row[5].ToString();
            return d;
        }

        protected override async Task<List<Drill>> CreateListModelAsync(List<object[]> rows)
        {
            List<Drill> drills = new List<Drill>();
            foreach (object[] item in rows)
            {
                Drill d;
                d = (Drill)await CreateModelAsync(item);
                drills.Add(d);
            }
            return drills;
        }

        public async Task<List<Drill>> GetAllAsync()
        {
            return ((List<Drill>)await SelectAllAsync());
        }
        public async Task<bool> insertcoach(Drill d)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
            {"name", d.name },
            {"muscle", d.muscle},
            {"sets", d.sets },
            {"reps", d.reps },
            {"duration", d.duration},
            {"description",d.description },
            };
            int num = await base.InsertAsync(data);
            if (num > 0)
            {
                return true;
            }
            else { return false; }
        }




        public async Task<int> UpdateAsync(Drill d)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {

                {"name", d.name },
                {"muscle", d.muscle},
                {"sets", d.sets },
                {"reps", d.reps },
                {"duration", d.duration},
                {"description",d.description },

            };
            filterValues.Add("name", d.name);
            return await base.UpdateAsync(fillValues, filterValues);
        }

        public async Task<int> DeleteAsync(Drill d)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>
            {
                { "name", d.name }
            };
            return await base.DeleteAsync(filterValues);
        }

        public async Task<Drill> SelectByPkAsync(string name)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("name",name);
            List<Drill> list = (List<Drill>)await SelectAllAsync(p);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }
        public async Task<List<Drill>> SelectByMuscleAsync(string muscle)
        {
            string sql = @"SELECT * FROM sportsync_db.drills where muscle = @muscle;";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("muscle", muscle);
            List<Drill> list = (List<Drill>)await SelectAllAsync(sql, p);
            return list;
        }



       
    }
}

