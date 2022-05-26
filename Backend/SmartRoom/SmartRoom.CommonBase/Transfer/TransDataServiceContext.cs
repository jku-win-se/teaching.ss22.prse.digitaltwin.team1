using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.CommonBase.Transfer
{
    public class TransDataServiceContext
    {
        private readonly ServiceRoutesManager _serviceRoutes;
        public TransDataServiceContext(ServiceRoutesManager serviceRoutes)
        {
            _serviceRoutes = serviceRoutes;
        }

        public async Task<MeasureState> GetRecentMeasureStateBy(Guid entityId, string measureType) 
        {
            return await Utils.WebApiTrans.GetAPI<MeasureState>($"{_serviceRoutes.TransDataService}ReadMeasure/GetRecentBy/{entityId}&{measureType}",
                _serviceRoutes.ApiKey);
        }

        public async Task<BinaryState> GetRecentBinaryStateBy(Guid entityId, string measureType)
        {
            return await Utils.WebApiTrans.GetAPI<BinaryState>($"{_serviceRoutes.TransDataService}ReadBinary/GetRecentBy/{entityId}&{measureType}",
                _serviceRoutes.ApiKey);
        }

        public async Task AddBinaryStates(State<bool>[] states)
        {
            await Utils.WebApiTrans.PostAPI($"{_serviceRoutes.TransDataService}TransWrite/AddBinaryState", states, _serviceRoutes.ApiKey);
        }

        public async Task AddMeasureStates(State<double>[] states)
        {
            await Utils.WebApiTrans.PostAPI($"{_serviceRoutes.TransDataService}TransWrite/AddMeasureState", states, _serviceRoutes.ApiKey);
        }
    }
}
