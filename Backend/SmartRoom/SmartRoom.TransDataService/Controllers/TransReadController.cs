using Microsoft.AspNetCore.Mvc;
using SmartRoom.CommonBase.Core.Contracts;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Core.Exceptions;
using SmartRoom.TransDataService.Logic.Contracts;

namespace SmartRoom.TransDataService.Controllers
{
    public class ReadMeasureController : TransReadController<MeasureState> { public ReadMeasureController(IReadManager manager) : base(manager) { } }
    public class ReadBinaryController : TransReadController<BinaryState> { public ReadBinaryController(IReadManager manager) : base(manager) { } }


    [Route("api/[controller]")]
    [ApiController]
    public abstract class TransReadController<S> : ControllerBase where S : class, IState, new()
    {
        private readonly IReadManager _manager;
        public TransReadController(IReadManager manager)
        {
            _manager = manager;
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<S[]>> GetBy(Guid id)
        {
            try
            {
                return Ok(await _manager.GetStatesByEntityID<S>(id));
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }
        }

        [HttpGet("[action]/{id}&{name}")]
        public async Task<ActionResult<S>> GetRecentBy(Guid id, string name)
        {
            try
            {
                return Ok(await _manager.GetRecentStateByEntityID<S>(id, name));
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<string[]>> GetTypesBy(Guid id)
        {
            try
            {
                return Ok(await _manager.GetStateTypesByEntityID<S>(id));
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }
        }

        [HttpGet("[action]/{id}&{name}")]
        public async Task<ActionResult<object>> GetChartData(Guid id, string name, int intervall = 5, int daySpan = 1)
        {
            if (!(await _manager.GetStateTypesByEntityID<S>(id)).Any(ms => ms.Equals(name))) return BadRequest("Parameter *name* does not exsist with the given ID!");
            if (intervall < 0) intervall *= -1;
            if (intervall == 0) intervall = 5;
            try
            {
                return Ok(await _manager.GetChartData<S>(id, name, intervall, daySpan));
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }

        }

        [HttpPost("[action]/{name}")]
        public async Task<ActionResult<object>> GetChartData([FromBody] Guid[] ids, string name, int intervall = 5, int daySpan = 1)
        {
            ids = ids.Where(id => _manager.GetStateTypesByEntityID<S>(id).Result.Any(ms => ms.Equals(name))).ToArray();

            if (!ids.Any()) return BadRequest("Parameter *name* does not exsist with the given IDs!");
            if (intervall < 0) intervall *= -1;
            if (intervall == 0) intervall = 5;
            try
            {
                return Ok(await _manager.GetChartData<S>(ids, name, intervall, daySpan));
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }
        }
    }
}
