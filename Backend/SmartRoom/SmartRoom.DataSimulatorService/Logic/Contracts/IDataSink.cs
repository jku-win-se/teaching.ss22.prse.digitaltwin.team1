using Serilog.Core;
using Serilog.Events;

namespace SmartRoom.DataSimulatorService.Logic.Contracts
{
    public interface IDataSink : ILogEventSink
    {
        List<LogEvent> Events { get; set; }
    }
}