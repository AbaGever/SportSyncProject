using Microsoft.AspNetCore.Mvc;
using DBL2;
using Models2;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportSyncAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        



        [HttpGet]
        [ActionName("GetAllCustomer")]
        public async Task<List<Trainer>> Get()
        {
            TrainerDB tdb = new TrainerDB();
            List<Trainer> TrainerList;
            TrainerList = await tdb.GetAllAsync();
            return TrainerList;
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
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // POST api/<ToDoListController>
        [HttpPost]
        [ActionName("Login")]
        public async Task<ActionResult<Trainer>> Post2([FromBody] Trainer item)
        {
            if (item is null) return BadRequest();
            TrainerDB UserDB = new TrainerDB();
            Trainer User = await UserDB.LoginAsync(item.emailaddress, item.password); if (User == null)
            {
                return BadRequest("User not found");
            }
            else
            {
                return Ok(User);
            }
        } // POST api/<ToDoListController>
        [HttpPost]
        [ActionName("Login1")]
        public async Task<ActionResult<Coach>> Post3([FromBody] Coach item)
        {
            if (item is null) return BadRequest();
            CoachDB UserDB = new CoachDB();
            Coach User = await UserDB.LoginAsync(item.emailaddress, item.password); if (User == null)
            {
                return BadRequest("User not found");
            }
            else
            {
                return Ok(User);
            }
        }
    }
}
