using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.DataSimulatorService.Contracts;
using SmartRoom.DataSimulatorService.Models;

namespace SmartRoom.DataSimulatorService.Logic
{
    public class SensorManager
    {
        private string _baseDataServiceURL => _configuration["Services:BaseDataService"];
        private string _transDataServiceURL => _configuration["Services:TransDataService"];
        private string _apiKey => _configuration["ApiKey"];

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
            _rooms = await CommonBase.Utils.WebApiTrans.GetAPI<List<Room>>($"{_baseDataServiceURL}room", _apiKey);
            _roomEquipment = await CommonBase.Utils.WebApiTrans.GetAPI<List<RoomEquipment>>($"{_baseDataServiceURL}roomequipment", _apiKey);
            _rooms.ForEach(r =>
            {
                _sensors.Add(r.Id,
                    CommonBase.Utils.WebApiTrans.GetAPI<List<string>>($"{_transDataServiceURL}ReadMeasure/GetTypesBy/{r.Id}", _apiKey).GetAwaiter().GetResult()
                    .Select(d => new Models.MeasureSensor(CommonBase.Utils.WebApiTrans.GetAPI<MeasureState>($"{_transDataServiceURL}ReadMeasure/GetRecentBy/{r.Id}&{d}", _apiKey).GetAwaiter().GetResult()))
                    .ToArray());
            });
            _roomEquipment.ForEach(re =>
            {
                _sensors.Add(re.Id,
                    CommonBase.Utils.WebApiTrans.GetAPI<List<string>>($"{_transDataServiceURL}ReadBinary/GetTypesBy/{re.Id}", _apiKey).GetAwaiter().GetResult()
                    .Select(d => new Models.BinarySensor(CommonBase.Utils.WebApiTrans.GetAPI<BinaryState>($"{_transDataServiceURL}ReadBinary/GetRecentBy/{re.Id}&{d}", _apiKey).GetAwaiter().GetResult()))
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

            foreach (var item in _sensors.Where(m => m.Value.Any()))
            {
                MeasureSensor?[] measureSensors = item.Value.Where(i => i.GetType().Equals(typeof(MeasureSensor))).ToArray().Any()
                    ? item.Value.Where(i => i.GetType().Equals(typeof(MeasureSensor))).Select(s => s as MeasureSensor).ToArray()
                    : new MeasureSensor[0];

                BinarySensor?[] binarySensors = item.Value.Where(i => i.GetType().Equals(typeof(BinarySensor))).ToArray().Any()
                    ? item.Value.Where(i => i.GetType().Equals(typeof(BinarySensor))).Select(s => s as BinarySensor).ToArray()
                    : new BinarySensor[0];

                foreach (var sensor in measureSensors)
                {
                    tasks.Add(Task.Run(() => Task.Run(() => measureStates.AddRange(GenerateMissingDataForSensor<MeasureState, MeasureSensor, double>(sensor!, item.Key)))));
                }

                foreach (var sensor in binarySensors)
                {
                    tasks.Add(Task.Run(() => binaryStates.AddRange(GenerateMissingDataForSensor<BinaryState, BinarySensor, bool>(sensor!, item.Key))));
                }
            }

            Task.WaitAll(tasks.ToArray());
            tasks.Clear();

            if (binaryStates.Any())
            {
                tasks.Add(Task.Run(async () =>
                {
                    await CommonBase.Utils.WebApiTrans.PostAPI($"{_transDataServiceURL}TransWrite/AddBinaryState", binaryStates.ToArray(), _apiKey);
                    _logger.LogInformation($"[Simulator] [Added {binaryStates.Count()} BinaryStates]");
                }));
            }

            if (measureStates.Any()) 
            {
                tasks.Add(Task.Run(async () =>
                {
                    await CommonBase.Utils.WebApiTrans.PostAPI($"{_transDataServiceURL}TransWrite/AddMeasureState", measureStates.ToArray(), _apiKey);
                    _logger.LogInformation($"[Simulator] [Added {measureStates.Count()} MeasureStates]");
                }));
            }
             
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
                _logger.LogInformation(" [Actor]" + sensor.ToString());
            }
        }

        private IEnumerable<ST> GenerateMissingDataForSensor<ST, SE, T>(SE sensor, Guid key) where SE : Sensor<T> where ST : State<T>, new()
        {
            List<ST> sTs = new List<ST>();
            DateTime start = DateTime.UtcNow.AddHours(-12);
            Random random = new Random();   

            if (start < sensor!.TimeStamp) start = sensor.TimeStamp;
            while (start < DateTime.UtcNow)
            {
                if (sTs is BinaryState && random.Next(1, 10) > 8) sensor.ChangeState(start);
                else if (sTs is MeasureState) sensor.ChangeState(start);
                
                sTs.Add(new ST
                {
                    EntityRefID = key,
                    Name = sensor.Type,
                    TimeStamp = start,
                    Value = sensor.Value,
                });
                start = start.AddMinutes(2);
            }

            return sTs;
        }
    }
}
