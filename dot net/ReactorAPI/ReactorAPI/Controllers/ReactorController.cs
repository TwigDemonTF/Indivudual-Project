using Logic.DTO_s;
using Logic.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReactorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactorController : ControllerBase
    {
        private readonly ReactorService _reactorService;

        public ReactorController(ReactorService reactorService)
        {
            _reactorService = reactorService;
        }

        // GET: api/<ReactorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ReactorController>/5
        [HttpGet("{userId}")]
        public string Get(int userId)
        {
            return "value";
        }

        // POST api/<ReactorController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ReactorHistoryDTO reactorHistoryDto)
        {
            try
            {
                await _reactorService.AddReactorData(reactorHistoryDto);
                return Ok(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ReactorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReactorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
