using SmartRoom.CommonBase.Transfer.Contracts;

namespace SmartRoom.CommonBase.Transfer
{
    public class DataSimulatorContext : IDataSimulatorContext
    {
        private readonly IServiceRoutesManager _serviceRoutes;

        public DataSimulatorContext(IServiceRoutesManager serviceRoutes)
        {
            _serviceRoutes = serviceRoutes;
        }

        public async Task SetAllBinariesForRoomByEqipmentType(Guid roomId, string equipmentType, bool binaryValue)
        {
            await Utils.WebApiTrans.GetAPI<object>($"{_serviceRoutes.DataSimulatorService}command/SetAllBianriesForRoomByEquipmentType/{roomId}&{equipmentType}&{binaryValue}", _serviceRoutes.ApiKey);
        }

    }
}
