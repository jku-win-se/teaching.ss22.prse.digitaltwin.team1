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

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<BinaryState[]>> GetBinaryBy(Guid id)
        {
            return await _manager.GetStatesByEntityID<BinaryState>(id);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<MeasureState[]>> GetMeasureBy(Guid id)
        {
            return await _manager.GetStatesByEntityID<MeasureState>(id);
        }

        //GetRecentDataByID

        //[HttpGet("[action]/{id}/{interval}")]
        //GetAgregateDataByID(interval = 5)
    }
}
