using SmartRoom.CommonBase.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRoom.CSVConsole.Models
{
    public class DoorOpen : IBaseModel<BinaryState>
    {
        public DateTime Timestamp { get; set; }
        public int Door_Id { get; set; }
        public bool isOpen { get; set; }

        public BinaryState GetEntity()
        {
            return new BinaryState
            {
                BinaryValue = isOpen,
                Name = typeof(DoorOpen).Name,
                TimeStamp = Timestamp
            };
        }
    }
}
