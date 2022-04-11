namespace SmartRoom.CSVConsole.Models
{
    public class Room : IBaseModel<CommonBase.Core.Entities.Room>
    {
        public string name { get; set; } = string.Empty;
        public int size { get; set; }

        public CommonBase.Core.Entities.Room GetEntity()
        {
            return new CommonBase.Core.Entities.Room
            {
                Name = name,
                Size = size
            };
        }
    }
}
