using Microsoft.AspNetCore.Mvc;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.TransDataService.Logic;

namespace SmartRoom.TransDataService.Controllers
{
    public class ReadMeasureController : TransReadController<MeasureState> { public ReadMeasureController(ReadManager manager) : base(manager) { } }
    public class ReadBinaryController : TransReadController<BinaryState> { public ReadBinaryController(ReadManager manager) : base(manager) { } }


    [Route("api/[controller]")]
    [ApiController]
    public abstract class TransReadController<S> : ControllerBase where S : State
    {
        private ReadManager _manager;
        public TransReadController(ReadManager manager)
        {
            _manager = manager;
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<S[]>> GetBy(Guid id)
        {
            return await _manager.GetStatesByEntityID<S>(id);
        }


        [HttpGet("[action]/{id}&{name}")]
        public async Task<ActionResult<S>> GetRecentBy(Guid id, string name)
        {
            return await _manager.GetRecentStateByEntityID<S>(id, name);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<string[]>> GetTypesBy(Guid id)
        {
            return await _manager.GetStateTypesByEntityID<S>(id);
        }


        [HttpGet("[action]/{id}&{name}")]
        public async Task<object> GetChartData(Guid id, string name, int intervall = 5, int daySpan = 1)
        {
            if (!(await _manager.GetStateTypesByEntityID<S>(id)).Any(ms => ms.Equals(name))) return BadRequest("Parameter *name* does not exsist with the given ID!");
            if (intervall < 0) intervall *= -1;
            if (intervall == 0) intervall = 5;

            return await _manager.GetChartData<S>(id, name, intervall, daySpan);
        }

        [HttpPost("[action]/{name}")]
        public async Task<object> GetChartData([FromBody] Guid[] ids, string name, int intervall = 5, int daySpan = 1)
        {
            ids = ids.Where(id => _manager.GetStateTypesByEntityID<S>(id).Result.Any(ms => ms.Equals(name))).ToArray();

            if (!ids.Any()) return BadRequest("Parameter *name* does not exsist with the given IDs!");
            if (intervall < 0) intervall *= -1;
            if (intervall == 0) intervall = 5;

            return await _manager.GetChartData<S>(ids, name, intervall, daySpan);
        }
    }
}
