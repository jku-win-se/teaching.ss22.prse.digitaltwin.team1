using Microsoft.AspNetCore.Mvc;
using SmartRoom.CommonBase.Core.Exceptions;
using SmartRoom.DataSimulatorService.Logic.Contracts;

namespace SmartRoom.DataSimulatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ISensorManager _sensorManager;
        private readonly IHostApplicationLifetime _host;

        public CommandController(ISensorManager sensorManager, IHostApplicationLifetime host)
        {
            _sensorManager = sensorManager;
            _host = host;
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

        [HttpGet("[action]/{roomId}&{equipmentType}&{val}")]
        public IActionResult SetAllBianriesForRoomByEquipmentType(Guid roomId, string equipmentType, bool val)
        {
            try
            {
                _sensorManager.SetAllBinariesByRoom(roomId, equipmentType, val);
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }
            return Ok();
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
