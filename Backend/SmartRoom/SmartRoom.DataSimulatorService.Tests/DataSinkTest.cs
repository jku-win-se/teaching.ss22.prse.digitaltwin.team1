using Serilog.Events;
using Serilog.Parsing;
using SmartRoom.DataSimulatorService.Logic;
using System.Collections.Generic;
using Xunit;

namespace SmartRoom.DataSimulatorService.Tests
{
    public class DataSinkTest
    {
        [Fact]
        public void Ctor_Ok()
        {
            var sink = new DataSink();
            Assert.NotNull(sink);
        }

        [Fact]
        public void Events_GetSet_NotNull()
        {
            var sink = new DataSink();
            sink.Events.Add(new LogEvent(new System.DateTimeOffset(), LogEventLevel.Information, new System.Exception(), new MessageTemplate(new List<MessageTemplateToken>()), new List<LogEventProperty>()));
            Assert.Single(sink.Events);
        }

        [Fact]
        public void Events_NotNull()
        {
            var sink = new DataSink();
            try
            {
                sink.Emit(new LogEvent(new System.DateTimeOffset(), LogEventLevel.Information, new System.Exception(), new MessageTemplate(new List<MessageTemplateToken>()), new List<LogEventProperty>()));
            }
            catch (System.Exception e)
            {
                Assert.Null(e);
            }

        }
    }
}
