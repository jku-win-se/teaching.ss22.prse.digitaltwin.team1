using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.CommonBase.Transfer.Contracts
{
    public interface IBaseDataServiceContext
    {
        Task<List<RoomEquipment>> GetRoomEquipments();
        Task<List<Room>> GetRooms();
    }
}