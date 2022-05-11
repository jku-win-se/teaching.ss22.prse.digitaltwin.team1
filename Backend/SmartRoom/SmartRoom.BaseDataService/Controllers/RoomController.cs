using Microsoft.AspNetCore.Mvc;
using SmartRoom.BaseDataService.Models;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Core.Exceptions;
using SmartRoom.CommonBase.Logic.Contracts;
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
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SpecialRoomModelForOurFEDev entity)
        {
            if (entity == null) return BadRequest(Messages.PARAMTER_NULL);
            try
            {
                await _entityManager.Add(entity.GetRoom());
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }
            return Ok();
        }
    }
}
