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

        public async Task LoadData()
        {
            _rooms = await CommonBase.Utils.WebApiTrans.GetAPI<List<CommonBase.Core.Entities.Room>>($"{_configuration["Services:BaseDataService"]}room", _configuration["ApiKey"]);
            _roomEquipment = await CommonBase.Utils.WebApiTrans.GetAPI<List<CommonBase.Core.Entities.RoomEquipment>>($"{_configuration["Services:BaseDataService"]}roomequipment", _configuration["ApiKey"]);
        }
    }
}
