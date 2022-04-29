using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.DataSimulatorService.Logic
{
    public class DataManager
    {
        private IConfiguration _configuration;
        private ILogger<DataManager> _logger;
        private List<Room> _rooms;
        private List<RoomEquipment> _roomEquipment;
        private Dictionary<Guid, Contracts.ISensor[]> _sensors;

        public DataManager(IConfiguration configuration, ILogger<DataManager> logger)
        {
            _rooms = new List<Room>();
            _roomEquipment = new List<RoomEquipment>();
            _sensors = new Dictionary<Guid, Contracts.ISensor[]>();
            _configuration = configuration;
            _logger = logger;
        }

        public async Task LoadData()
        {
            _rooms = await CommonBase.Utils.WebApiTrans.GetAPI<List<Room>>($"{_configuration["Services:BaseDataService"]}room", _configuration["ApiKey"]);
            _roomEquipment = await CommonBase.Utils.WebApiTrans.GetAPI<List<RoomEquipment>>($"{_configuration["Services:BaseDataService"]}roomequipment", _configuration["ApiKey"]);
            _rooms.ForEach(r =>
            {
                _sensors.Add(r.Id, CommonBase.Utils.WebApiTrans.GetAPI<List<string>>($"{_configuration["Services:TransDataService"]}ReadMeasure/GetTypesBy/{r.Id}", _configuration["ApiKey"]).GetAwaiter().GetResult()
                    .Select(d => new Models.MeasureSensor
                    {
                        Type = d,
                        Value = CommonBase.Utils.WebApiTrans.GetAPI<MeasureState>($"{_configuration["Services:TransDataService"]}ReadMeasure/GetRecentBy/{r.Id}&{d}", _configuration["ApiKey"]).GetAwaiter().GetResult().MeasureValue
                    }).ToArray());
            });
            _roomEquipment.ForEach(re =>
            {
                _sensors.Add(re.Id, CommonBase.Utils.WebApiTrans.GetAPI<List<string>>($"{_configuration["Services:TransDataService"]}ReadBinary/GetTypesBy/{re.Id}", _configuration["ApiKey"]).GetAwaiter().GetResult()
                    .Select(d => new Models.BinarySensor
                    {
                        Type = d,
                        Value = CommonBase.Utils.WebApiTrans.GetAPI<BinaryState>($"{_configuration["Services:TransDataService"]}ReadBinary/GetRecentBy/{re.Id}&{d}", _configuration["ApiKey"]).GetAwaiter().GetResult().BinaryValue
                    }).ToArray());
            });
            _logger.LogInformation("[DataManager] [BaseData loaded]");
        }

        public void GenerateData()
        {
            List<Task> tasks = new List<Task>();
            Random random = new Random();
            foreach (var item in _sensors.Where(m => m.Value.Any()))
            {
                foreach(var sensor in item.Value)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        Thread.Sleep(random.Next(10, 2000));
                        sensor.RenewData();
                        _logger.LogInformation(sensor.ToString());
                    }));
                }
            }
            Task.WaitAll(tasks.ToArray());
        }
    }
}
