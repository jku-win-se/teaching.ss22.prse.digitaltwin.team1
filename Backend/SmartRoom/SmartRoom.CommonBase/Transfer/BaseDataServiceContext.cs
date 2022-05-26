using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.CommonBase.Transfer
{
    public class BaseDataServiceContext
    {
        private readonly ServiceRoutesManager _serviceRoutes;
        public BaseDataServiceContext(ServiceRoutesManager serviceRoutes)
        {
            _serviceRoutes = serviceRoutes;
        }

        public async Task<List<Room>> GetRooms()
        {
            return await Utils.WebApiTrans.GetAPI<List<Room>>($"{_serviceRoutes.BaseDataService}room", _serviceRoutes.ApiKey);
        }

        public async Task<List<RoomEquipment>> GetRoomEquipments()
        {
            return await Utils.WebApiTrans.GetAPI<List<RoomEquipment>>($"{_serviceRoutes.BaseDataService}roomequipment", _serviceRoutes.ApiKey);
        }
    }
}
