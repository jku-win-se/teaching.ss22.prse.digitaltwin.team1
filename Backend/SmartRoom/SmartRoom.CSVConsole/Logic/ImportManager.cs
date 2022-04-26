using SmartRoom.CommonBase.Utils;
using SmartRoom.CSVConsole.Models;

namespace SmartRoom.CSVConsole.Logic
{
    public class ImportManager
    {
        private string _path;
        private IEnumerable<WindowOpen> _windowStates;
        private IEnumerable<Door> _doors;
        private IEnumerable<DoorConnectsRoom> _doorConnectsRoom;
        private IEnumerable<DoorOpen> _doorOpen;
        private IEnumerable<PeopleInRoom> _peopleInRoom;
        private IEnumerable<Room> _roomCap;
        private IEnumerable<Ventilator> _ventilator;
        private IEnumerable<VentilatorOn> _ventilatorOn;
        private IEnumerable<Window> _window;
        private List<SmartRoom.CommonBase.Core.Entities.Room> _rooms;

        public ImportManager(string path)
        {
            _path = path;
           
            ConfigureManager();
        }

        public async void ImportCSV ()
        {
            try
            {
                ImportData();
            }
            catch (Exception)
            {

                throw new Exception("Import fehlgeschlagen");
            }
           
            
            
            
            AddEquipmentToRoom();

            try
            {
                foreach (var room in _rooms)
                {
                    var res = await SmartRoom.CommonBase.Utils.WebApiTrans.PostAPI("https://basedataservice.azurewebsites.net/api/Room", room, "bFR9bGhOi0n0ccoEhrhsE57VrHjkJJz9");
                    if (!res.IsSuccessStatusCode) throw new Exception();
                }
            }
            catch (Exception)
            {

                throw new Exception("Raum hinzufuegen beim Base-Data-Service fehlgeschlagen");
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
                    equipments.Add(_doors.Where(d => d.ID.Equals(doorInRoom.Door_ID)).First().GetEntity());
                }

                equipments.AddRange(_window.Where(w => w.Room_Id.Equals(roomModel.name, StringComparison.CurrentCultureIgnoreCase)).Select(w => w.GetEntity()).ToList());

                roomEntity.RoomEquipment.AddRange(equipments);
                _rooms.Add(roomEntity);
            }

        }


        private void ConfigureManager()
        {
            _rooms = new List<SmartRoom.CommonBase.Core.Entities.Room>();
            _peopleInRoom = new List<PeopleInRoom>();
        }

        private void ImportData()
        {
            using (GenericCSVReader<WindowOpen> reader = new GenericCSVReader<WindowOpen>(@$"{_path}\WindowOpen.csv"))
            {
                _windowStates = reader.Read();
            }

            using (GenericCSVReader<Door> reader = new GenericCSVReader<Door>(@$"{_path}\Door.csv"))
            {
                _doors = reader.Read();
            }

            using (GenericCSVReader<DoorConnectsRoom> reader = new GenericCSVReader<DoorConnectsRoom>(@$"{_path}\Door_Connects_Room.csv"))
            {
                _doorConnectsRoom = reader.Read();
            }

            using (GenericCSVReader<DoorOpen> reader = new GenericCSVReader<DoorOpen>(@$"{_path}\DoorOpen.csv"))
            {
                _doorOpen = reader.Read();
            }

            using (GenericCSVReader<PeopleInRoom> reader = new GenericCSVReader<PeopleInRoom>(@$"{_path}\PeopleInRoom.csv"))
            {
                _peopleInRoom = reader.Read();
            }

            using (GenericCSVReader<Room> reader = new GenericCSVReader<Room>(@$"{_path}\Room.csv"))
            {
                _roomCap = reader.Read();
            }

            using (GenericCSVReader<Ventilator> reader = new GenericCSVReader<Ventilator>(@$"{_path}\Ventilator.csv"))
            {
                _ventilator = reader.Read();
            }

            using (GenericCSVReader<VentilatorOn> reader = new GenericCSVReader<VentilatorOn>(@$"{_path}\VentilatorOn.csv"))
            {
                _ventilatorOn = reader.Read();
            }

            using (GenericCSVReader<Window> reader = new GenericCSVReader<Window>(@$"{_path}\Window.csv"))
            {
                _window = reader.Read();
            }

       

        }

    }
}
