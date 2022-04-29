using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.DataSimulatorService.Logic
{
    public class DataManager
    {
        private IConfiguration _configuration;
        private ILogger<DataManager> _logger;
        private List<CommonBase.Core.Entities.Room> _rooms;
        private List<CommonBase.Core.Entities.RoomEquipment> _roomEquipment;
        private Dictionary<Guid, string[]> _binaryStateTypes;
        private Dictionary<Guid, string[]> _measureStateTypes;

        public DataManager(IConfiguration configuration, ILogger<DataManager> logger)
        {
            _rooms = new List<CommonBase.Core.Entities.Room>();
            _roomEquipment = new List<CommonBase.Core.Entities.RoomEquipment>();
            _binaryStateTypes = new Dictionary<Guid, string[]>();
            _measureStateTypes = new Dictionary<Guid, string[]>();
            _configuration = configuration;
            _logger = logger;
        }

        public async Task LoadData()
        {
            _rooms = await CommonBase.Utils.WebApiTrans.GetAPI<List<CommonBase.Core.Entities.Room>>($"{_configuration["Services:BaseDataService"]}room", _configuration["ApiKey"]);
            _roomEquipment = await CommonBase.Utils.WebApiTrans.GetAPI<List<CommonBase.Core.Entities.RoomEquipment>>($"{_configuration["Services:BaseDataService"]}roomequipment", _configuration["ApiKey"]);
            _rooms.ForEach(r =>
            {
                _measureStateTypes.Add(r.Id, CommonBase.Utils.WebApiTrans.GetAPI<List<string>>($"{_configuration["Services:TransDataService"]}ReadMeasure/GetTypesBy/{r.Id}", _configuration["ApiKey"]).GetAwaiter().GetResult().ToArray());
            });
            _roomEquipment.ForEach(re =>
            {
                _binaryStateTypes.Add(re.Id, CommonBase.Utils.WebApiTrans.GetAPI<List<string>>($"{_configuration["Services:TransDataService"]}ReadBinary/GetTypesBy/{re.Id}", _configuration["ApiKey"]).GetAwaiter().GetResult().ToArray());
            });
            _logger.LogInformation("[DataManager] [data loaded]");
        }

        public async Task GenerateData()
        {
            foreach (var item in _measureStateTypes.Where(m => m.Value.Any()))
            {
                foreach (var type in item.Value) 
                {
                    var state = await CommonBase.Utils.WebApiTrans.GetAPI<MeasureState>($"{_configuration["Services:TransDataService"]}ReadMeasure/GetRecentBy/{item.Key}&{type}", _configuration["ApiKey"]);
                    var val = GenerateMeasureData(state.MeasureValue);
                    _logger.LogInformation($"[Sensor: {item.Key}] [{type}] [Value: {val}]");
                }
            }
        }

        private double GenerateMeasureData(double val)
        {
            Random random = new Random();
            if (DateTime.Now > DateTime.Parse("09:00") && DateTime.Now < DateTime.Parse("19:00"))
            {
                if (random.Next(1, 10) > 3)
                {
                    val += random.NextDouble();
                }
                else val -= random.NextDouble();
            }
            else
            {
                if (random.Next(1, 10) > 3)
                {
                    val -= random.NextDouble();
                }
                else val += random.NextDouble();
            }

            return val;
        }
    }
}
