using Microsoft.AspNetCore.Mvc;

using DBL2;
using Models2;
using System;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportSyncAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {


        [HttpGet("{trainerid:int},{d:int},{m:int},{y:int}")]
        [ActionName("GetDailyW")]
        public async Task<List<Workout>> GetDW(int trainerid, int d,int m,int y)
        {
            DateTime tm = new DateTime(y, m, d);
            string td = tm.ToString("yyyy-MM-dd");
            DateTime t = DateTime.ParseExact(td, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            WorkoutDB wdb = new WorkoutDB();
            List<Workout> workouts = await wdb.GetWorkoutsDailyAsync(trainerid, t);
            return workouts;
        }


   
        // POST api/<ToDoListController>
        [HttpPost]
        [ActionName("Register")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> Post1([FromBody] Trainer item)
        {
            TrainerDB db = new TrainerDB();
            bool b;
            b = await db.insertuser(item);
            if (b)
            {
                return item.id;
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK);
            }
        }

        // POST api/<ToDoListController>
        [HttpPost]
        [ActionName("Log")]
        public async Task<ActionResult<Trainer>> Post2([FromBody] TrainerNew item)
        {
            TrainerDB UserDB = new TrainerDB();
            Trainer User = await UserDB.LoginAsync(item.email, item.pass);
            if (User == null)
            {
                return BadRequest("User not found");

            }

            return Ok(User);

        } // POST api/<ToDoListController>


       // POST api/<ToDoListController>

        [HttpPut]
        [ActionName("Ch")]
        public async Task<ActionResult<Trainer>>Change([FromBody] Trainer t)
        {
            TrainerDB UserDB = new TrainerDB();
            int n = await UserDB.UpdateAsyncWithoutGroup(t);
            if (n <= 0) {
                return BadRequest("Couldnt Update User");
                    }
            return StatusCode(StatusCodes.Status200OK); ;

        }





    }
}
