using Microsoft.AspNetCore.Mvc;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Core.Exceptions;
using SmartRoom.TransDataService.Logic.Contracts;

namespace SmartRoom.TransDataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransWriteController : ControllerBase
    {
        private readonly IWriteManager _manager;
        public TransWriteController(IWriteManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddBinaryState(BinaryState[] state)
        {
            try
            {
                await _manager.addState(state);
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddMeasureState(MeasureState[] state)
        {
            try
            {
                await _manager.addState(state);
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }
            return Ok();
        }
    }
}
