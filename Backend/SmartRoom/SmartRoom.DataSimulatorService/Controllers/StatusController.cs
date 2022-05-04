using Microsoft.AspNetCore.Mvc;
using SmartRoom.CommonBase.Core.Exceptions;
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
            try
            {
                return Ok(_sink.Events.Select(s => s.RenderMessage()).ToArray());
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }
        }

        [HttpGet("[action]")]
        public ActionResult<string> GetSimulatorStatus()
        {
            try
            {
                return Ok(_sink.Events.Select(s => s.RenderMessage()).Where(m => m.Contains("[Simulator]")).Last());
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }
        }
    }
}
