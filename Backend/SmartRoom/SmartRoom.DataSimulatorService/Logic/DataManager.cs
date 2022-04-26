namespace SmartRoom.DataSimulatorService.Logic
{
    public class DataManager
    {
        private IConfiguration _configuration;
        private IEnumerable<CommonBase.Core.Entities.Room> _rooms;
        private IEnumerable<CommonBase.Core.Entities.RoomEquipment> _roomEquipment;
        private Dictionary<Guid, string[]> _stateTypes;

        public DataManager(IConfiguration configuration)
        {
            _rooms = new List<CommonBase.Core.Entities.Room>();
            _roomEquipment = new List<CommonBase.Core.Entities.RoomEquipment>();
            _stateTypes = new Dictionary<Guid, string[]>();
            _configuration = configuration;
        }

        public void LoadData()
        {
            //_rooms = CommonBase.Utils.WebApiTrans.GetAPI<CommonBase.Core.Entities.Room>(_configuration["Services:BaseDataService"], _configuration["ApiKey"]);
        }
    }
}
