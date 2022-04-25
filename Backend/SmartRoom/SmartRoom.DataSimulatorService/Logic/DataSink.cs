using Serilog.Core;
using Serilog.Events;

namespace SmartRoom.DataSimulatorService.Logic
{
    public class DataSink : ILogEventSink
    {
        public List<LogEvent> Events { get; set; } = new List<LogEvent>();
        public void Emit(LogEvent logEvent)
        {
            Events.Add(logEvent);
        }
    }
}
