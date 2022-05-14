using Microsoft.AspNetCore.Mvc;
using SmartRoom.CommonBase.Core.Exceptions;
using SmartRoom.DataSimulatorService.Logic;

namespace SmartRoom.DataSimulatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly DataSink _sink;
        private readonly IHostApplicationLifetime _host;

        public StatusController(DataSink sink, IHostApplicationLifetime host)
        {
            _sink = sink;
            _host = host;
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

        [HttpGet("[action]")]
        public IActionResult StopService()
        {
            try
            {
                _host.StopApplication();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }
        }
    }
}
