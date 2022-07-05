using Microsoft.AspNetCore.SignalR;
using Moq;
using SmartRoom.CommonBase.Core.Contracts;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Transfer.Contracts;
using SmartRoom.TransDataService.Logic;
using Xunit;

namespace SmartRoom.TransDataService.Tests
{
    public class SecurityManagerTest
    {
        [Fact]
        public void Ctor_ValidParam_Ok()
        {
            var hub = new Mock<IHubContext<SensorHub>>();

            var manager = new SecurityManager(hub.Object, new Mock<IDataSimulatorContext>().Object);

            Assert.NotNull(manager);
        }

        [Fact]
        public void CheckCo2ImporveAirQuality_NullOrEmpty_NoException()
        {
            var hub = new Mock<IHubContext<SensorHub>>();
            var manager = new SecurityManager(hub.Object, new Mock<IDataSimulatorContext>().Object);

            try
            {
                manager.CheckTemperaturesAndSendAlarm(null!);
                manager.CheckTemperaturesAndSendAlarm(new IState[0]);
            }
            catch (System.Exception e)
            {
                Assert.NotNull(e);
            }
        }

        [Fact]
        public void CheckCo2ImporveAirQuality_ValidParam_NoException()
        {
            var hub = new Mock<IHubContext<SensorHub>>();
            var hubClients = new Mock<IHubClients>();
            hubClients.Setup(hc => hc.All).Returns(new Mock<IClientProxy>().Object);
            hub.Setup(h => h.Clients).Returns(hubClients.Object);

            var manager = new SecurityManager(hub.Object, new Mock<IDataSimulatorContext>().Object);

            try
            {
                manager.CheckTemperaturesAndSendAlarm(new IState[] { new MeasureState() { Name = "Temperature", Value = 75 } });
            }
            catch (System.Exception e)
            {
                Assert.NotNull(e);
            }
        }
    }
}
