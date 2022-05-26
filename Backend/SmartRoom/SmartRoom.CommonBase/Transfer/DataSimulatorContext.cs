namespace SmartRoom.CommonBase.Transfer
{
    public class DataSimulatorContext
    {
        private readonly ServiceRoutesManager _serviceRoutes;

        public DataSimulatorContext(ServiceRoutesManager serviceRoutes)
        {
            _serviceRoutes = serviceRoutes;
        }

        public async Task SetAllBinariesForRoomByEqipmentType(Guid roomId, string equipmentType, bool binaryValue) 
        {
            await Utils.WebApiTrans.GetAPI<object>($"{_serviceRoutes.DataSimulatorService}command/SetAllBianriesForRoomByEquipmentType/{roomId}&{equipmentType}&{binaryValue}", _serviceRoutes.ApiKey);
        }
        
    }
}
