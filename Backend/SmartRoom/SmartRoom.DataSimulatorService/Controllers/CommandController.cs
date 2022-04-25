using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartRoom.DataSimulatorService.Logic;

namespace SmartRoom.DataSimulatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private DataSink _sink;

        public CommandController(DataSink sink)
        {
            _sink = sink;
        }
        [HttpGet]
        public ActionResult<string[]> GetBy()
        {
            return _sink.Events.Select(s => s.RenderMessage()).ToArray();
        }
    }
}
