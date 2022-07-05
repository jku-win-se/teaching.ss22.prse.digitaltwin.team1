using SmartRoom.BaseDataService.Persistence.Contracts;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Persistence;
using SmartRoom.CommonBase.Persistence.Contracts;

namespace SmartRoom.BaseDataService.Persistence
{
    public class SmartRoomUOW : UnitOfWork, ISmartRoomUOW
    {
        public SmartRoomUOW(SmartRoomDBContext context) : base(context)
        {
            Rooms = new RoomRepository(context);
            RoomEquipments = new GenericEntityRepository<RoomEquipment>(context);
        }

        public IGenericEntityRepository<Room> Rooms { get; private set; }

        public IGenericEntityRepository<RoomEquipment> RoomEquipments { get; private set; }
    }
}
