using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models2;
using Org.BouncyCastle.Utilities;
namespace DBL2
{
    public class DrillsListDB : BaseDB<drillslist>
    {
        protected override string GetTableName()
        {
            return "drillslist";
        }

        protected override List<string> GetPrimaryKeyName()
        {
            return new List<string> { "Workoutid", "Drillname" };
        }

        protected override async Task<drillslist> CreateModelAsync(object[] row)
        {
            drillslist g = new drillslist();
            try
            {
                g.Workoutid = int.Parse(row[0].ToString());
                g.Drillname = row[1].ToString();
            }
            catch (Exception ex)
            { g = null; }


            return g;
        }

        protected override async Task<List<drillslist>> CreateListModelAsync(List<object[]> rows)
        {
            List<drillslist> drillslist = new List<drillslist>();
            foreach (object[] item in rows)
            {
                drillslist g;
                g = (drillslist)await CreateModelAsync(item);
                drillslist.Add(g);
            }
            return drillslist;
        }
        public async Task<bool> InsertDLAsync(drillslist dl)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
            {"Workoutid",dl.Workoutid },
            {"Drillname" , dl.Drillname},
            };
            int num = await base.InsertAsync(data);
            if (num > 0)
            {
                return true;
            }
            else { return false; }
        }
        public async Task<int> DeleteDLAsync(drillslist dl)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>
            {
            {"Workoutid",dl.Workoutid },
            {"Drillname", dl.Drillname},
            };
            return await base.DeleteAsync(filterValues);
        }
        public async Task<List<drillslist>> SelectAllInWorkoutAsync(int Workoutid)
        {
            string q = "SELECT * FROM sportsync_db.drillslist WHERE Workoutid=@Workoutid;";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Workoutid", Workoutid);
            List<drillslist> list = await SelectAllAsync(q, p);
            return list;
        }
        public async Task<List<string>> GetDrillsNamesInWorkOut(int Workoutid)
        {
            List<drillslist> result = await SelectAllInWorkoutAsync(Workoutid);

            if (result == null)
            {
                return null;
            }

            List<string> drillNames = new List<string>();
            foreach (var drill in result)
            {
                drillNames.Add(drill.Drillname);
            }

            return drillNames;
        }
    }
}
