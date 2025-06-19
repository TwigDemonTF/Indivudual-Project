using Logic.DTO_s;
using Logic.Services;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("{reactorId}")]
        public ActionResult<ReactorValuesDTO> Get(int reactorId)
        {
            try
            {
                ReactorValuesDTO? reactorValuesDto = _reactorService.GetReactorValues(reactorId);
                if (reactorValuesDto == null)
                {
                    return NotFound();
                }

                return Ok(reactorValuesDto); // 200 with JSON body
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
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
        public async Task<IActionResult> Put(int id, [FromBody] ReactorValuesDTO reactorValuesDto)
        {
            try
            {
                bool success = await _reactorService.UpdateReactorValues(id, reactorValuesDto);
                if (success)
                {
                    return Ok(new { success = true, message = "Reactor values updated successfully." });
                }
                else
                {
                    return NotFound(new { success = false, message = "Reactor not found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // DELETE api/<ReactorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [Authorize]
        [HttpGet("Latest")]
        public IActionResult GetLatestData([FromQuery] int reactorId)
        {
            if (reactorId <= 0)
                return BadRequest(new { error = "Invalid reactorId" });

            // Get Amsterdam timezone safely (Linux and Windows support)
            var amsterdamTimeZone = GetTimeZone("Europe/Amsterdam");

            // Current time in Amsterdam
            var amsterdamNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, amsterdamTimeZone);

            // Get cutoff (24 hours ago in Amsterdam time)
            var amsterdam24HoursAgo = amsterdamNow.AddHours(-24);

            // Convert to UTC for database querying
            var cutoffUtc = TimeZoneInfo.ConvertTimeToUtc(amsterdam24HoursAgo, amsterdamTimeZone);

            // Filter by reactorId and time
            var recentData = _reactorService.GetLatestReactorData(cutoffUtc, reactorId)
                .Where(r => r.TimeStamp > cutoffUtc && r.ReactorId == reactorId)
                .OrderBy(r => r.TimeStamp)
                .ToList();

            if (recentData == null || !recentData.Any())
                return NotFound(new { error = "No data available" });

            return Ok(recentData);
        }

        private static TimeZoneInfo GetTimeZone(string timezoneId)
        {
            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById(timezoneId); // For Linux/Mac
            }
            catch (TimeZoneNotFoundException)
            {
                // Map Linux/IANA -> Windows timezones manually if needed
                if (timezoneId == "Europe/Amsterdam")
                    return TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time"); // For Windows

                throw;
            }
        }
    }
}
