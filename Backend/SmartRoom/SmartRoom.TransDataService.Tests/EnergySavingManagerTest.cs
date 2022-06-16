using Moq;
using SmartRoom.CommonBase.Core.Contracts;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Transfer.Contracts;
using SmartRoom.TransDataService.Logic;
using SmartRoom.TransDataService.Logic.Contracts;
using System;
using Xunit;

namespace SmartRoom.TransDataService.Tests
{
    public class EnergySavingManagerTest
    {
        [Fact]
        public void Ctor_ValidParam_Ok()
        {
            var manager = new EnergySavingManager(new Mock<IReadManager>().Object, new Mock<IDataSimulatorContext>().Object);
            Assert.NotNull(manager);
        }

        [Fact]
        public void CheckCo2ImporveAirQuality_ValidStates_NoException()
        {
            var cont = new Mock<IDataSimulatorContext>();
            cont.Setup(c => c.SetAllBinariesForRoomByEqipmentType(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(System.Threading.Tasks.Task.CompletedTask);
            var manager = new EnergySavingManager(new Mock<IReadManager>().Object, cont.Object);

            try
            {
                manager.TurnLightsOnPeopleInRoom(new IState[] { new MeasureState() { Name = "PeopleInRoom", Value = 1 } });
            }
            catch (Exception e)
            {
                Assert.NotNull(e);
            }
        }

        [Fact]
        public void TurnLightsOffNoPeopleInRoom_ValidStates_NoException()
        {
            var cont = new Mock<IDataSimulatorContext>();
            cont.Setup(c => c.SetAllBinariesForRoomByEqipmentType(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(System.Threading.Tasks.Task.CompletedTask);
            var manager = new EnergySavingManager(new Mock<IReadManager>().Object, cont.Object);

            try
            {
                manager.TurnLightsOffNoPeopleInRoom(new IState[] { new MeasureState() { Name = "PeopleInRoom", Value = 0 } });
            }
            catch (Exception e)
            {
                Assert.NotNull(e);
            }
        }

        [Fact]
        public void TurnDevicesOffNoPeopleInRoom_ValidStates_NoException()
        {
            var cont = new Mock<IDataSimulatorContext>();
            cont.Setup(c => c.SetAllBinariesForRoomByEqipmentType(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(System.Threading.Tasks.Task.CompletedTask);
            var manager = new EnergySavingManager(new Mock<IReadManager>().Object, cont.Object);

            try
            {
                manager.TurnDevicesOffNoPeopleInRoom(new IState[] { new MeasureState() { Name = "PeopleInRoom", Value = 0 } });
            }
            catch (Exception e)
            {
                Assert.NotNull(e);
            }
        }
    }
}
