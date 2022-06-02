using Microsoft.AspNetCore.Mvc;
using SmartRoom.CommonBase.Core.Exceptions;
using SmartRoom.DataSimulatorService.Logic.Contracts;

namespace SmartRoom.DataSimulatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IDataSink _sink;

        public StatusController(IDataSink sink)
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
                return Ok(_sink.Events.Select(s => s.RenderMessage()).Last(m => m.Contains("[Simulator]")));
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }
        }
    }
}
