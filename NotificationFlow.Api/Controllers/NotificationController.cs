using Microsoft.AspNetCore.Mvc;
using NotificationFlow.Business.Interfaces.Services;
using NotificationFlow.Business.Models;

namespace NotificationFlow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NotificationRequest request, [FromServices] INotificationService service)
        {
            await service.Post(request);
            return Ok();
        }
    }
}
