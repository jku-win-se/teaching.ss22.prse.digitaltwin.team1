using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRoom.CSVConsole.Models
{
    public class WindowOpen
    {
        public DateTime Timestamp { get; set; }
        public int Window_ID { get; set; }
        public bool isOpen { get; set; }
    }
}
