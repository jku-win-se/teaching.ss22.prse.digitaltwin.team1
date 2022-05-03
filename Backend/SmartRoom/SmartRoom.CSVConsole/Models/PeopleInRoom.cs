using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.CSVConsole.Models
{
    public class PeopleInRoom : IBaseModel <MeasureState>
    {
        public DateTime Timestamp { get; set; }
        public string Room_Id { get; set; } = string.Empty;
        public int NOPeopleInRoom { get; set; }

        public MeasureState GetEntity()
        {
            return new MeasureState
            {
                Value = NOPeopleInRoom,
                Name = typeof(PeopleInRoom).Name,
                TimeStamp = Timestamp
            };
        }
    }
}
