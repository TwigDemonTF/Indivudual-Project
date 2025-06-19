using Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace ReactorAPI.Controllers
{
    public class NotificationController : Controller
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
