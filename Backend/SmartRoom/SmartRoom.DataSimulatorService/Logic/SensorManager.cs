using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.DataSimulatorService.Logic
{
    public class SensorManager
    {
        private IConfiguration _configuration;
        private ILogger<SensorManager> _logger;
        private List<Room> _rooms;
        private List<RoomEquipment> _roomEquipment;
        private Dictionary<Guid, Contracts.ISensor[]> _sensors;

        public SensorManager(IConfiguration configuration, ILogger<SensorManager> logger)
        {
            _rooms = new List<Room>();
            _roomEquipment = new List<RoomEquipment>();
            _sensors = new Dictionary<Guid, Contracts.ISensor[]>();
            _configuration = configuration;
            _logger = logger;
        }

        public async Task Init()
        {
            _rooms = await CommonBase.Utils.WebApiTrans.GetAPI<List<Room>>($"{_configuration["Services:BaseDataService"]}room", _configuration["ApiKey"]);
            _roomEquipment = await CommonBase.Utils.WebApiTrans.GetAPI<List<RoomEquipment>>($"{_configuration["Services:BaseDataService"]}roomequipment", _configuration["ApiKey"]);
            _rooms.ForEach(r =>
            {
                _sensors.Add(r.Id, 
                    CommonBase.Utils.WebApiTrans.GetAPI<List<string>>($"{_configuration["Services:TransDataService"]}ReadMeasure/GetTypesBy/{r.Id}", _configuration["ApiKey"]).GetAwaiter().GetResult()
                    .Select(d => new Models.MeasureSensor(CommonBase.Utils.WebApiTrans.GetAPI<MeasureState>($"{_configuration["Services:TransDataService"]}ReadMeasure/GetRecentBy/{r.Id}&{d}", _configuration["ApiKey"]).GetAwaiter().GetResult()))
                    .ToArray());
            });
            _roomEquipment.ForEach(re =>
            {
                _sensors.Add(re.Id, 
                    CommonBase.Utils.WebApiTrans.GetAPI<List<string>>($"{_configuration["Services:TransDataService"]}ReadBinary/GetTypesBy/{re.Id}", _configuration["ApiKey"]).GetAwaiter().GetResult()
                    .Select(d => new Models.BinarySensor(CommonBase.Utils.WebApiTrans.GetAPI<BinaryState>($"{_configuration["Services:TransDataService"]}ReadBinary/GetRecentBy/{re.Id}&{d}", _configuration["ApiKey"]).GetAwaiter().GetResult()))
                    .ToArray());
            });
            _logger.LogInformation("[DataManager] [BaseData loaded]");
        }

        public void GenerateData()
        {
            List<Task> tasks = new List<Task>();
            Random random = new Random();

            foreach (var item in _sensors.Where(m => m.Value.Any()))
            {
                foreach (var sensor in item.Value.Where(i => i.GetType().Equals(typeof(Models.MeasureSensor))))
                {
                    tasks.Add(Task.Run(() =>
                    {
                        Thread.Sleep(random.Next(10, 2000));
                        sensor.ChangeState();
                        _logger.LogInformation(sensor.ToString());
                    }));
                }
            }
            Task.WaitAll(tasks.ToArray());
        }

        public void GenerateMissingData()
        {
            List<MeasureState> measureStates = new List<MeasureState>();
            List<BinaryState> binaryStates = new List<BinaryState>();

            List<Task> tasks = new List<Task>();
            Random random = new Random();

            foreach (var item in _sensors.Where(m => m.Value.Any()))
            {
                Models.MeasureSensor?[] measureSensors = item.Value.Where(i => i.GetType().Equals(typeof(Models.MeasureSensor))).ToArray().Any()
                    ? item.Value.Where(i => i.GetType().Equals(typeof(Models.MeasureSensor))).Select(s => s as Models.MeasureSensor).ToArray()
                    : new Models.MeasureSensor[0];

                Models.BinarySensor?[] binarySensors = item.Value.Where(i => i.GetType().Equals(typeof(Models.BinarySensor))).ToArray().Any()
                    ? item.Value.Where(i => i.GetType().Equals(typeof(Models.BinarySensor))).Select(s => s as Models.BinarySensor).ToArray()
                    : new Models.BinarySensor[0];

                foreach (var sensor in measureSensors)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        DateTime start = DateTime.UtcNow.AddHours(-12);
                        if (start < sensor!.TimeStamp) start = sensor.TimeStamp;
                        while (start < DateTime.UtcNow) 
                        {
                            sensor.ChangeState();
                            measureStates.Add(new MeasureState
                            {
                                EntityRefID = item.Key,
                                MeasureValue = sensor.Value,
                                Name = sensor.Type,
                                TimeStamp = start
                            });
                            start = start.AddMinutes(2);
                        }                      
                    }));
                }

                foreach (var sensor in binarySensors)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        DateTime start = DateTime.UtcNow.AddHours(-12);
                        if (start < sensor!.TimeStamp) start = sensor.TimeStamp;
                        while (start < DateTime.UtcNow)
                        {
                            if (random.Next(1, 10) > 8) sensor.ChangeState();
                            binaryStates.Add(new BinaryState
                            {
                                EntityRefID = item.Key,
                                BinaryValue = sensor.Value,
                                Name = sensor.Type,
                                TimeStamp = start
                            });
                            start = start.AddMinutes(2);
                        }
                    }));
                }
            }
            Task.WaitAll(tasks.ToArray());
            tasks.Clear();

            if (binaryStates.Any())
                tasks.Add(CommonBase.Utils.WebApiTrans.PostAPI($"{_configuration["Services:TransDataService"]}TransWrite/AddBinaryState", binaryStates.ToArray(), _configuration["ApiKey"]));
            if (measureStates.Any())
                tasks.Add(CommonBase.Utils.WebApiTrans.PostAPI($"{_configuration["Services:TransDataService"]}TransWrite/AddMeasureState", measureStates.ToArray(), _configuration["ApiKey"]));
            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (Exception e)
            {

                _logger.LogError(e.Message);
            }

        }

        public void ChangeState(Guid id, string type)
        {
            var sensor = _sensors[id].Where(s => s.Type.Equals(type)).First();

            if (sensor != null)
            {
                sensor.ChangeState();
                _logger.LogInformation(" [Act]" + sensor.ToString());
            }
        }
    }
}
