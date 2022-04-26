using Microsoft.AspNetCore.Mvc;
using SmartRoom.DataSimulatorService.Logic;

namespace SmartRoom.DataSimulatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private DataSink _sink;

        public StatusController(DataSink sink)
        {
            _sink = sink;
        }
        [HttpGet("[action]")]
        public ActionResult<string[]> GetLogs()
        {
            return _sink.Events.Select(s => s.RenderMessage()).ToArray();
        }

        [HttpGet("[action]")]
        public ActionResult<string> GetSimulatorStatus()
        {
            return _sink.Events.Select(s => s.RenderMessage()).Where(m => m.Contains("[Simulator]")).Last();
        }
    }
}
