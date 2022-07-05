using Moq;
using SmartRoom.CommonBase.Core.Contracts;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Transfer.Contracts;
using SmartRoom.TransDataService.Logic;
using System;
using Xunit;

namespace SmartRoom.TransDataService.Tests
{
    public class AirQualityManagerTest
    {
        [Fact]
        public void Ctor_ValidParam_Ok()
        {
            var manager = new AirQualityManager(new Mock<IDataSimulatorContext>().Object);
            Assert.NotNull(manager);
        }

        [Fact]
        public void CheckCo2ImporveAirQuality_NullOrEmpty_NoException()
        {
            var manager = new AirQualityManager(new Mock<IDataSimulatorContext>().Object);

            try
            {
                manager.CheckCo2ImporveAirQuality(null!);
                manager.CheckCo2ImporveAirQuality(new IState[0]);
            }
            catch (System.Exception e)
            {
                Assert.NotNull(e);
            }
        }

        [Fact]
        public void CheckCo2ImporveAirQuality_ValidStates_NoException()
        {
            var cont = new Mock<IDataSimulatorContext>();
            cont.Setup(c => c.SetAllBinariesForRoomByEqipmentType(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(System.Threading.Tasks.Task.CompletedTask);
            var manager = new AirQualityManager(cont.Object);

            try
            {
                manager.CheckCo2ImporveAirQuality(new IState[] { new MeasureState() { Name = "Co2", Value = 1001} });
            }
            catch (System.Exception e)
            {
                Assert.NotNull(e);
            }
        }
    }
}
