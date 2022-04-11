using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRoom.CSVConsole.Models
{
    public class PeopleInRoom
    {
        public DateTime Timestamp { get; set; }
        public string Room_Id { get; set; }
        public int NOPeopleInRoom { get; set; }
    }
}
