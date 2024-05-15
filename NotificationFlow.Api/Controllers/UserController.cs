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
            return Created();
        }
    }
}
