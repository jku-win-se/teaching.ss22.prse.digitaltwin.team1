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

            if (logEvent.RenderMessage().Contains("[Act] [Sensor]")) Console.ForegroundColor = ConsoleColor.Green;
            else if (logEvent.RenderMessage().Contains("[Sensor]")) Console.ForegroundColor = ConsoleColor.Yellow;
            else Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine($"[{logEvent.Timestamp}] [{logEvent.Level}] {logEvent.RenderMessage()}");
        }
    }
}
