using Microsoft.AspNetCore.Mvc;

namespace SmartRoom.DataSimulatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        [HttpGet("[action]/{id}&{stateType}")]
        public IActionResult ChangeBianry(Guid id, string stateType)
        {
            return Ok();
        }
    }
}
