﻿using Microsoft.AspNetCore.Mvc;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Logic.Contracts;
using SmartRoom.CommonBase.Web;

namespace SmartRoom.BaseDataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomEquipmentController : GenericEntityController<RoomEquipment>
    {
        public RoomEquipmentController(IGenericEntityManager<RoomEquipment> entityManager) : base(entityManager)
        {
        }
    }
}
