using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRoom.CSVConsole.Models
{
    public class VentilatorOn
    {
        public DateTime Timestamp { get; set; }
        public int VentilatorId { get; set; }
        public bool isOn { get; set; }
    }
}
