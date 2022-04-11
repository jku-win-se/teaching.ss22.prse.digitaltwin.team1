using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.TransDataService.Logic;

namespace SmartRoom.TransDataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransReadController : ControllerBase
    {
        private ReadManager _manager;
        public TransReadController(ReadManager manager)
        {
            _manager = manager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BinaryState[]>> GetBy(Guid id)
        {
            return await _manager.GetStatesByEntityID(id);
        }
    }
}
