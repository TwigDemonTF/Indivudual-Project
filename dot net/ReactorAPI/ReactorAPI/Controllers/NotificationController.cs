using Logic.DTO_s;
using Logic.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace ReactorAPI.Controllers
{
    public class NotificationController : Controller
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpDelete("Notification/{notificationId}")]
        public IActionResult Delete(int notificationId)
        {
            try
            {
                bool success = _notificationService.DeleteNotification(notificationId);
                if (success)
                {
                    return StatusCode(200, new { success = true, message = "Successfully deleted notification" });
                }
                Console.WriteLine("Bruh");
                return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("Notification/{userId}")]
        public IActionResult Get(int userId)
        {
            try
            {
                List<NotificationDTO> notificationDTOs = _notificationService.GetNotifications(userId);
                if (notificationDTOs == null)
                {
                    return NotFound();
                }

                return Ok(notificationDTOs); // 200 with JSON body
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
