using Microsoft.AspNetCore.Mvc;
using NotificationFlow.Business.Interfaces.Services;
using NotificationFlow.Business.Models;

namespace NotificationFlow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRequest request, [FromServices] IUserService service)
        {
            await service.Post(request);
            return Ok();
        }

        [HttpPatch]
        [Route("notification-preferences")]
        public async Task<IActionResult> PatchNotificationPreferences([FromBody] UserNotificationPreferencesRequest request, [FromServices] IUserService service)
        {
            await service.PatchNotificationPreferences(request);
            return Ok();
        }

        [HttpGet]
        [Route("{Id}/notification")]
        public async Task<IActionResult> GetUserNotifications([FromRoute] GetUserNotificationsRequest request, [FromServices] IUserService service)
        {
            var notifications = await service.GetUserNotifications(request);
            return Ok(notifications);
        }
    }
}
