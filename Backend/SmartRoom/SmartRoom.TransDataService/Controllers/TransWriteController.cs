using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.TransDataService.Logic;

namespace SmartRoom.TransDataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransWriteController : ControllerBase
    {
        private WriteManager _manager;
        public TransWriteController(WriteManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public async Task<IActionResult> AddState(BinaryState state)
        {
            try
            {
                await _manager.addState(state);
            }
            catch (Exception e)
            {
                return BadRequest(e);
                throw;
            }
            return Ok();
        }
    }
}
