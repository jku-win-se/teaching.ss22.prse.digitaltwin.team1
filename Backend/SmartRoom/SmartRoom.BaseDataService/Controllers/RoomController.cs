using Microsoft.AspNetCore.Mvc;
using SmartRoom.BaseDataService.Models;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Core.Exceptions;
using SmartRoom.CommonBase.Logic.Contracts;
using SmartRoom.CommonBase.Utils;
using SmartRoom.CommonBase.Web;

namespace SmartRoom.BaseDataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : GenericEntityController<Room>
    {
        public RoomController(IGenericEntityManager<Room> entityManager) : base(entityManager)
        {
        }

        [HttpGet("Models")]
        public async Task<ActionResult<IEnumerable<SpecialRoomModelForOurFEDev>>> GetModels()
        {
            try
            {
                return Ok((await _entityManager.Get()).Select(r => new SpecialRoomModelForOurFEDev(r)));
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }
        }

        [HttpPost("Models")]
        public async Task<ActionResult> PostModel([FromBody] SpecialRoomModelForOurFEDev model)
        {
            if (model == null) return BadRequest(Messages.PARAMTER_NULL);
            try
            {
                await _entityManager.Add(model.GetRoom());
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }
            return Ok();
        }

        [HttpPut("Models")]
        public async Task<ActionResult> PutModel([FromBody] SpecialRoomModelForOurFEDev model)
        {
            if (model == null) return BadRequest(Messages.PARAMTER_NULL);
            try
            {
                var roomToUpdate = await _entityManager.GetBy(model.Id);
                var newRoom = model.GetRoom();             

                foreach (var re in model.RoomEquipmentDict)
                {
                    var count = roomToUpdate.RoomEquipment.Count(rre => rre.Name.Equals(re.Key));
                    while (count > re.Value)
                    {
                        roomToUpdate.RoomEquipment.Remove(roomToUpdate.RoomEquipment.Last(rre => rre.Name.Equals(re.Key)));
                        count--;
                    }
                    if (count < re.Value) 
                    {
                        roomToUpdate.RoomEquipment.AddRange(newRoom.RoomEquipment.Where(rre => rre.Name.Equals(re.Key)).Take(re.Value - count));
                    }
                }
                GenericMapper.MapObjects(roomToUpdate, model);
                await _entityManager.Update(roomToUpdate);
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }
            return Ok();
        }
    }
}
