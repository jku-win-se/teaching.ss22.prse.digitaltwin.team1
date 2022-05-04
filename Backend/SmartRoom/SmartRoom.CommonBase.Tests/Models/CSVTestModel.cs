using System;

namespace SmartRoom.CommonBase.Tests.Models
{
    public class CSVTestModel : IComparable<CSVTestModel>
    {
        public DateTime Timestamp { get; set; }
        public int Door_Id { get; set; }
        public bool isOpen { get; set; }
        public string Room_Id { get; set; } = string.Empty;
        public double Temperature { get; set; }


        public int CompareTo(CSVTestModel? toCompare)
        {
            if (Timestamp.Equals(toCompare?.Timestamp) && Door_Id.Equals(toCompare.Door_Id) && isOpen.Equals(toCompare.isOpen) && Room_Id.Equals(toCompare.Room_Id) && Temperature.Equals(toCompare.Temperature)) return 0;
            else return 1;
        }

        
    }


}
