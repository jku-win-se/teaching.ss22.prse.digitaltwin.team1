namespace SmartRoom.CommonBase.Core.Entities
{
    public abstract class State : EntityObject
    {
        public string Name { get; set; } = "isTriggerd";
        public int? RoomEquipmentID { get; set; }
        public int? RoomID { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
