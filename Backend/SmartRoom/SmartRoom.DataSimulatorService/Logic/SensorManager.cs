using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.DataSimulatorService.Contracts;
using SmartRoom.DataSimulatorService.Models;

namespace SmartRoom.DataSimulatorService.Logic
{
    public class SensorManager
    {
        private readonly Dictionary<string, string[]> _binaryTypes = new Dictionary<string, string[]>
        {
            {"Ventilator", new string[] {"IsOn"}},
            {"Light", new string[] {"IsOn"}},
            {"Window", new string[] {"IsOpen"}},
            {"Door", new string[] {"IsOpen"}},
        };
        private readonly string[] _measureTypes = new string[] { "Temperature", "Co2", "PeopleInRoom" };

        private readonly Dictionary<Guid, ISensor[]> _sensors;
        private readonly ILogger<SensorManager> _logger;
        private readonly IConfiguration _configuration;
        private string _baseDataServiceURL => _configuration["Services:BaseDataService"];
        private string _transDataServiceURL => _configuration["Services:TransDataService"];
        private string _apiKey => _configuration["ApiKey"];
        private bool _loadingBaseData;

        private List<Room> _rooms;
        private List<RoomEquipment> _roomEquipment;

        public SensorManager(IConfiguration configuration, ILogger<SensorManager> logger)
        {
            _rooms = new List<Room>();
            _roomEquipment = new List<RoomEquipment>();
            _sensors = new Dictionary<Guid, ISensor[]>();
            _configuration = configuration;
            _logger = logger;
        }

        public async Task Init()
        {
            _loadingBaseData = true;
            _rooms = await CommonBase.Utils.WebApiTrans.GetAPI<List<Room>>($"{_baseDataServiceURL}room", _apiKey);
            _roomEquipment = await CommonBase.Utils.WebApiTrans.GetAPI<List<RoomEquipment>>($"{_baseDataServiceURL}roomequipment", _apiKey);
            _rooms.ForEach(r =>
            {
                _sensors.Add(r.Id,
                    _measureTypes
                    .Select(d => new Models.MeasureSensor(StateUpdated!, CommonBase.Utils.WebApiTrans.GetAPI<MeasureState>($"{_transDataServiceURL}ReadMeasure/GetRecentBy/{r.Id}&{d}", _apiKey).GetAwaiter().GetResult()))
                    .ToArray());
            });
            _roomEquipment.ForEach(re =>
            {
                if (_binaryTypes.ContainsKey(re.Name))
                {
                    _sensors.Add(re.Id, _binaryTypes[re.Name]
                        .Select(d => new Models.BinarySensor(StateUpdated!, CommonBase.Utils.WebApiTrans.GetAPI<BinaryState>($"{_transDataServiceURL}ReadBinary/GetRecentBy/{re.Id}&{d}", _apiKey).GetAwaiter().GetResult()))
                        .ToArray());
                }
            });
            _logger.LogInformation("[DataManager] [BaseData loaded]");
        }

        public void GenerateData()
        {
            List<Task> tasks = new List<Task>();

            foreach (var item in _sensors.Where(m => m.Value.Any()))
            {
                foreach (var sensor in item.Value.Where(i => i.GetType().Equals(typeof(MeasureSensor))))
                {
                    tasks.Add(Task.Run(() =>
                    {
                        Thread.Sleep(new Random().Next(10, 59000));
                        sensor.ChangeState();
                    }));
                }
            }
            Task.WaitAll(tasks.ToArray());
        }

        public async Task GenerateMissingData()
        {
            List<MeasureState> measureStates = new();
            List<BinaryState> binaryStates = new();
            List<MeasureSensor> measureSensors = new();
            List<BinarySensor> binarySensors = new();

            List<Task> tasks = new List<Task>();

            foreach (var item in _sensors.Where(m => m.Value.Any()))
            {
                measureSensors.AddRange(GetConcreteSensors<MeasureSensor>(item.Value)!);
                binarySensors.AddRange(GetConcreteSensors<BinarySensor>(item.Value)!);
            }

            foreach (var sensor in measureSensors)
            {
                tasks.Add(Task.Run(() =>
                {
                    var data = GenerateMissingDataForSensor<MeasureState, MeasureSensor, double>(sensor!);
                    if (data.Any()) lock (measureStates) measureStates.AddRange(data);
                }));
            }

            foreach (var sensor in binarySensors)
            {
                tasks.Add(Task.Run(() =>
                {
                    var data = GenerateMissingDataForSensor<BinaryState, BinarySensor, bool>(sensor!);
                    if (data.Any()) lock (binaryStates) binaryStates.AddRange(data);
                }));
            }

            Task.WaitAll(tasks.ToArray());

            try
            {
                if (binaryStates.Any()) await CommonBase.Utils.WebApiTrans.PostAPI($"{_transDataServiceURL}TransWrite/AddBinaryState", binaryStates.ToArray(), _apiKey);
                if (measureStates.Any()) await CommonBase.Utils.WebApiTrans.PostAPI($"{_transDataServiceURL}TransWrite/AddMeasureState", measureStates.ToArray(), _apiKey);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            finally
            {
                _loadingBaseData = false;
            }
        }

        public void ChangeState(Guid id, string type)
        {
            var sensor = _sensors[id].Select(s => s as BinarySensor).First(s => s!.State.Name.Equals(type));
            if (sensor != null) sensor.ChangeState();
        }
        public void SetAllBinariesByRoom(Guid id, string type, bool val)
        {
            _rooms.First(r => r.Id.Equals(id))?.RoomEquipment.ForEach(r =>
            {
                _sensors[r.Id].Select(s => s as BinarySensor).First(s => s!.State.Name.Equals(type))?.ChangeState(val);
            });
        }
        private IEnumerable<ST> GenerateMissingDataForSensor<ST, SE, T>(SE sensor) where SE : Sensor<T> where ST : State<T>, new()
        {
            List<ST> sTs = new List<ST>();
            DateTime start = DateTime.UtcNow.AddHours(-72);
            Random random = new Random();

            if (start < sensor!.State.TimeStamp) start = sensor.State.TimeStamp;
            while (start < DateTime.UtcNow)
            {
                if (typeof(ST).Equals(typeof(BinaryState)) && random.Next(1, 20) > 18) sensor.ChangeState(start);
                else if (typeof(ST).Equals(typeof(MeasureState))) sensor.ChangeState(start);

                sTs.Add(new ST
                {
                    EntityRefID = sensor.State.EntityRefID,
                    Name = sensor.State.Name,
                    TimeStamp = start,
                    Value = sensor.State.Value,
                });
                start = start.AddMinutes(2);
            }

            return sTs;
        }

        private void StateUpdated(object sender, EventArgs e)
        {
            if (!_loadingBaseData && sender is MeasureSensor)
            {
                var res = CommonBase.Utils.WebApiTrans.PostAPI($"{_transDataServiceURL}TransWrite/AddMeasureState", new State<double>[] {((MeasureSensor)sender).State}, _apiKey).GetAwaiter().GetResult();
            }
            _logger.LogInformation(sender.ToString());
        }

        private T?[] GetConcreteSensors<T>(ISensor[] sensors) where T : class, ISensor
        {
            if (sensors.Any()) return sensors.Where(i => i.GetType().Equals(typeof(T))).Select(s => s as T).ToArray();
            else return Array.Empty<T>();
        }
    }
}
