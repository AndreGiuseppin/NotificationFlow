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
        [Route("{id}/notification-preferences")]
        public async Task<IActionResult> PatchNotificationPreferences([FromRoute] int id,
            [FromBody] UserNotificationPreferencesRequest request, [FromServices] IUserService service)
        {
            await service.PatchNotificationPreferences(id, request);
            return Ok();
        }

        [HttpGet]
        [Route("{id}/notification")]
        public async Task<IActionResult> GetUserNotifications([FromRoute] int id, [FromServices] IUserService service)
        {
            var notifications = await service.GetUserNotifications(id);
            return Ok(notifications);
        }
    }
}
