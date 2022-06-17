using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog.Events;
using Serilog.Parsing;
using SmartRoom.DataSimulatorService.Controllers;
using SmartRoom.DataSimulatorService.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartRoom.DataSimulatorService.Tests
{
    public class StatusControllerTests
    {
        [Fact]
        public void Ctor_ValidParam_Ok()
        {
            var cont = new StatusController(new Mock<IDataSink>().Object);
            Assert.NotNull(cont);
        }

        [Fact]
        public void GetLogs_OkResult()
        {
            var mock = new Mock<IDataSink>();
            mock.Setup(d => d.Events).Returns(new List<LogEvent> { new LogEvent(new System.DateTimeOffset(), LogEventLevel.Information, new System.Exception(), new MessageTemplate(new List<MessageTemplateToken>()), new List<LogEventProperty>()) });
            Serilog.Log.Information("Test");

            var cont = new StatusController(mock.Object);
            var res = cont.GetLogs().Result;

            Assert.IsType<OkObjectResult>(res);
            Assert.Single((IEnumerable<string>)(res as OkObjectResult)!.Value!);
        }

        [Fact]
        public void GetSimulatorStatus_Null_BadRequest()
        {
            var mock = new Mock<IDataSink>();
            mock.Setup(d => d.Events).Returns(new List<LogEvent> { new LogEvent(new System.DateTimeOffset(), LogEventLevel.Information, new System.Exception(), new MessageTemplate(new List<MessageTemplateToken>()), new List<LogEventProperty>()) });
            Serilog.Log.Information("Test");

            var cont = new StatusController(mock.Object);
            var res = cont.GetSimulatorStatus().Result;

            Assert.IsType<BadRequestObjectResult>(res);
        }

        [Fact]
        public void GetLogs_ThrowException_BadRequest()
        {
            var mock = new Mock<IDataSink>();
            mock.Setup(d => d.Events).Throws<Exception>();
            Serilog.Log.Information("Test");

            var cont = new StatusController(mock.Object);
            var res = cont.GetLogs().Result;

            Assert.IsType<BadRequestObjectResult>(res);
        }
    }
}
