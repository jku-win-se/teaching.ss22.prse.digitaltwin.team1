using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Persistence.Contracts;

namespace SmartRoom.BaseDataService.Persistence.Contracts
{
    public interface ISmartRoomUOW
    {
        IGenericEntityRepository<RoomEquipment> RoomEquipments { get; }
        IGenericEntityRepository<Room> Rooms { get; }
    }
}