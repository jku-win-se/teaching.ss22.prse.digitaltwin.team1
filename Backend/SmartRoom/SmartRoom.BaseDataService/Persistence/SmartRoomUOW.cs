using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Persistence;

namespace SmartRoom.BaseDataService.Persistence
{
    public class SmartRoomUOW : UnitOfWork
    {
        public SmartRoomUOW(SmartRoomDBContext context) : base(context)
        {
            Rooms = new GenericEntityRepository<Room>(context);
            RoomEquipments = new GenericEntityRepository<RoomEquipment>(context);
        }

        public GenericEntityRepository<Room> Rooms { get; private set; }
        
        public GenericEntityRepository<RoomEquipment> RoomEquipments { get; private set; }
    }
}
