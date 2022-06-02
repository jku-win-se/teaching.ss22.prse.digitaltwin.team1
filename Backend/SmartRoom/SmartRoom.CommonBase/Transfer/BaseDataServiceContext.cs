using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Transfer.Contracts;

namespace SmartRoom.CommonBase.Transfer
{
    public class BaseDataServiceContext : IBaseDataServiceContext
    {
        private readonly IServiceRoutesManager _serviceRoutes;
        public BaseDataServiceContext(IServiceRoutesManager serviceRoutes)
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
