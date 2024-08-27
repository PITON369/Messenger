using Messenger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Messanger : ControllerBase
    {
        static List<Message> ListOfMessages = new List<Message>();
        //GET api/<Messanger>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            string OutputString = "Not found";
            if ((id < ListOfMessages.Count) && (id >= 0))
                OutputString = JsonConvert.SerializeObject(ListOfMessages[id]);
            Console.WriteLine($"Message requested № {id} : {OutputString}");
            return OutputString;
        }

        // POST api <Messanger>
        [HttpPost]
        public IActionResult SendMessage([FromBody] Message msg)
        {
            if (msg == null)
                return BadRequest();
            ListOfMessages.Add(msg);
            Console.WriteLine(String.Format($"Total messages: {ListOfMessages.Count} Sent message: {msg}"));
            return new OkResult();
        }
    }
}
