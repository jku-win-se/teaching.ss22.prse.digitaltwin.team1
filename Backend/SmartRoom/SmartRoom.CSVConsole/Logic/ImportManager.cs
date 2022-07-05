using SmartRoom.CommonBase.Utils;
using SmartRoom.CommonBase.Utils.Contracts;
using SmartRoom.CSVConsole.Models;

namespace SmartRoom.CSVConsole.Logic
{
    public class ImportManager
    {
        private readonly string _path = string.Empty;
        private IEnumerable<Door> _doors;
        private IEnumerable<DoorConnectsRoom> _doorConnectsRoom;
        private IEnumerable<Room> _roomCap;
        private IEnumerable<Ventilator> _ventilator;
        private IEnumerable<Window> _window;
        private readonly List<CommonBase.Core.Entities.Room> _rooms;

        public ImportManager(string path)
        {
            _path = path;
            _rooms = new List<CommonBase.Core.Entities.Room>();
            _doors = new List<Door>();
            _doorConnectsRoom = new List<DoorConnectsRoom>();
            _roomCap = new List<Room>();
            _ventilator = new List<Ventilator>();
            _window = new List<Window>();
        }

        public async Task ImportCSV()
        {
            try
            {
                ImportData();
            }
            catch (Exception)
            {

                throw new FileLoadException("Import fehlgeschlagen");
            }

            AddEquipmentToRoom();

            foreach (var room in _rooms)
            {
                try
                {
                    try
                    {
                        var res = await SmartRoom.CommonBase.Utils.WebApiTrans.PostAPI("https://basedataservice.azurewebsites.net/api/Room", room, "bFR9bGhOi0n0ccoEhrhsE57VrHjkJJz9");
                        if (!res.IsSuccessStatusCode) throw new Exception();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Raum hinzufuegen beim Base-Data-Service fehlgeschlagen");
                    }

                }
                catch (Exception)
                {

                    Console.WriteLine("Raum hinzufuegen beim Base-Data-Service fehlgeschlagen");
                }
            }
        }


        private void AddEquipmentToRoom()
        {
            foreach (var roomModel in _roomCap)
            {
                var roomEntity = roomModel.GetEntity();
                var equipments = _ventilator
                    .Where(v => v.Room_Id.Equals(roomModel.name, StringComparison.CurrentCultureIgnoreCase))
                    .Select(v => v.GetEntity()).ToList();

                foreach (var doorInRoom in _doorConnectsRoom.Where(d => d.Room_ID.Equals(roomModel.name, StringComparison.CurrentCultureIgnoreCase)))
                {
                    equipments.Add(_doors.First(d => d.ID.Equals(doorInRoom.Door_ID)).GetEntity());
                }

                equipments.AddRange(_window.Where(w => w.Room_Id.Equals(roomModel.name, StringComparison.CurrentCultureIgnoreCase)).Select(w => w.GetEntity()).ToList());

                roomEntity.RoomEquipment.AddRange(equipments);
                _rooms.Add(roomEntity);
            }

        }

        private void ImportData()
        {
            using (IGenericCSVReader<Door> reader = new GenericCSVReader<Door>(@$"{_path}\Door.csv"))
            {
                _doors = reader.Read();
            }

            using (IGenericCSVReader<DoorConnectsRoom> reader = new GenericCSVReader<DoorConnectsRoom>(@$"{_path}\Door_Connects_Room.csv"))
            {
                _doorConnectsRoom = reader.Read();
            }

            using (IGenericCSVReader<Room> reader = new GenericCSVReader<Room>(@$"{_path}\Room.csv"))
            {
                _roomCap = reader.Read();
            }

            using (IGenericCSVReader<Ventilator> reader = new GenericCSVReader<Ventilator>(@$"{_path}\Ventilator.csv"))
            {
                _ventilator = reader.Read();
            }

            using (IGenericCSVReader<Window> reader = new GenericCSVReader<Window>(@$"{_path}\Window.csv"))
            {
                _window = reader.Read();
            }
        }
    }
}
