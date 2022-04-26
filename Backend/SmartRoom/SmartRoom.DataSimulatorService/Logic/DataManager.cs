namespace SmartRoom.DataSimulatorService.Logic
{
    public class DataManager
    {
        private IEnumerable<CommonBase.Core.Entities.Room> _rooms;
        private IEnumerable<CommonBase.Core.Entities.RoomEquipment> _roomEquipment;
        private Dictionary<Guid, string[]> _stateTypes;

        public DataManager()
        {
            _rooms = new List<CommonBase.Core.Entities.Room>();
            _roomEquipment = new List<CommonBase.Core.Entities.RoomEquipment>();
            _stateTypes = new Dictionary<Guid, string[]>();
        }
    }
}
