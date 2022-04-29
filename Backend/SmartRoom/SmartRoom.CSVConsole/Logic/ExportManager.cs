﻿using SmartRoom.CommonBase.Utils;

namespace SmartRoom.CSVConsole.Logic
{
    public class ExportManager
    {
        public async void ExportCSV()
        {
            var exp = await WebApiTrans.GetAPI<List< SmartRoom.CommonBase.Core.Entities.RoomEquipment>>("https://basedataservice.azurewebsites.net/api/RoomEquipment", "bFR9bGhOi0n0ccoEhrhsE57VrHjkJJz9");

            using (GenericCSVWriter<SmartRoom.CommonBase.Core.Entities.RoomEquipment> GenericCSVWriter = new GenericCSVWriter<CommonBase.Core.Entities.RoomEquipment>(exp, ""))
            {
                GenericCSVWriter.WriteToCSV();
            }
        }
    }
}