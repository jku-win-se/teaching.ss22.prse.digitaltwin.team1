using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Moq;
using SmartRoom.DataSimulatorService.Controllers;
using SmartRoom.DataSimulatorService.Logic.Contracts;
using Xunit;

namespace SmartRoom.DataSimulatorService.Tests
{
    public class CommandControllerTest
    {
        [Fact]
        public void Ctor_ValidParam_Ok()
        {
            var cont = new CommandController(new Mock<ISensorManager>().Object, new Mock<IHostApplicationLifetime>().Object);
            Assert.NotNull(cont);
        }

        [Fact]
        public void ChangeBianry_ValidParams_OkResult()
        {
            var cont = new CommandController(new Mock<ISensorManager>().Object, new Mock<IHostApplicationLifetime>().Object);
            Assert.IsType<OkResult>(cont.ChangeBianry(new System.Guid(), "isTriggerd"));
        }

        [Fact]
        public void SetAllBianriesForRoomByEquipmentType_ValidParams_OkResult()
        {
            var cont = new CommandController(new Mock<ISensorManager>().Object, new Mock<IHostApplicationLifetime>().Object);
            Assert.IsType<OkResult>(cont.SetAllBianriesForRoomByEquipmentType(new System.Guid(), "isTriggerd", true));
        }

        [Fact]
        public void StopApplication__OkResult()
        {
            var cont = new CommandController(new Mock<ISensorManager>().Object, new Mock<IHostApplicationLifetime>().Object);
            Assert.IsType<OkResult>(cont.StopService());
        }
    }
}
