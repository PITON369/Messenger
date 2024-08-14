using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public class MessangerController : Controller
    {
        //GET api/<MessangerControler>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "Current id is " + id.ToString();
        }

        // POST api <MessangerControler>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }
    }
}
