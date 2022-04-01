using Microsoft.AspNetCore.Mvc;
using SmartRoom.CommonBase.Core.Entities;
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
    }
}
