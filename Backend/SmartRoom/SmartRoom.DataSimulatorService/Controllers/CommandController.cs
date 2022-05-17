using Microsoft.AspNetCore.Mvc;
using SmartRoom.CommonBase.Core.Exceptions;
using SmartRoom.DataSimulatorService.Logic;

namespace SmartRoom.DataSimulatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly SensorManager _sensorManager;

        public CommandController(SensorManager sensorManager)
        {
            _sensorManager = sensorManager;
        }
        [HttpGet("[action]/{id}&{stateType}")]
        public IActionResult ChangeBianry(Guid id, string stateType)
        {
            try
            {
                _sensorManager.ChangeState(id, stateType);
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }
            return Ok();
        }

        [HttpGet("[action]/{roomId}&{stateType}&{val}")]
        public IActionResult SetAllBianriesForRoomByStateType(Guid roomId, string stateType, bool val)
        {
            try
            {
                _sensorManager.SetAllBinariesByRoom(roomId, stateType, val);
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }
            return Ok();
        }
    }
}
