using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.CSVConsole.Models
{
    public class Door : IBaseModel<RoomEquipment>
    {
        public int ID { get; set; }
        public RoomEquipment GetEntity()
        {
            return new RoomEquipment
            {
                EquipmentRef = ID.ToString(),
                Name = typeof(Door).Name
            };
        }
    }
}
