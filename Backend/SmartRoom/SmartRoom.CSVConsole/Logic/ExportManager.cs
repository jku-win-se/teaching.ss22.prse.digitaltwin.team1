using SmartRoom.CommonBase.Utils;

namespace SmartRoom.CSVConsole.Logic
{
    public class ExportManager
    {
        private string _path = string.Empty;
        
        public ExportManager(string path)
        {   
            if (!string.IsNullOrEmpty(path)) _path = path;
        }
        public async Task ExportCSV()
        {
            var exp = await WebApiTrans.GetAPI<List< SmartRoom.CommonBase.Core.Entities.RoomEquipment>>("https://basedataservice.azurewebsites.net/api/RoomEquipment", "bFR9bGhOi0n0ccoEhrhsE57VrHjkJJz9");

            using (GenericCSVWriter<SmartRoom.CommonBase.Core.Entities.RoomEquipment> GenericCSVWriter = new GenericCSVWriter<CommonBase.Core.Entities.RoomEquipment>(exp, _path))
            {
                GenericCSVWriter.WriteToCSV();
            }


            var expRooms = await WebApiTrans.GetAPI<List<SmartRoom.CommonBase.Core.Entities.Room>>("https://basedataservice.azurewebsites.net/api/RoomEquipment", "bFR9bGhOi0n0ccoEhrhsE57VrHjkJJz9");

            using (GenericCSVWriter<SmartRoom.CommonBase.Core.Entities.Room> GenericCSVWriter = new GenericCSVWriter<CommonBase.Core.Entities.Room>(expRooms, _path))
            {
                GenericCSVWriter.WriteToCSV();
            }
        }
    }
}
