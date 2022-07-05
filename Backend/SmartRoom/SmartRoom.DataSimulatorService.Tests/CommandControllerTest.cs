using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Moq;
using SmartRoom.DataSimulatorService.Controllers;
using SmartRoom.DataSimulatorService.Logic.Contracts;
using System;
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
        public void ChangeBianry_ThrowException_BadRequest()
        {
            var mockManager = new Mock<ISensorManager>();
            mockManager.Setup(m => m.ChangeState(It.IsAny<Guid>(), It.IsAny<string>())).Throws<Exception>();
            var cont = new CommandController(mockManager.Object, new Mock<IHostApplicationLifetime>().Object);

            var res = cont.ChangeBianry(new Guid(), "test");

            Assert.IsType<BadRequestObjectResult>(res);
        }

        [Fact]
        public void SetAllBianriesForRoomByEquipmentType_ValidParams_OkResult()
        {
            var cont = new CommandController(new Mock<ISensorManager>().Object, new Mock<IHostApplicationLifetime>().Object);
            Assert.IsType<OkResult>(cont.SetAllBianriesForRoomByEquipmentType(new System.Guid(), "isTriggerd", true));
        }

        [Fact]
        public void SetAllBianriesForRoomByEquipmentType_ThrowException_BadRequest()
        {
            var mockManager = new Mock<ISensorManager>();
            mockManager.Setup(m => m.SetAllBinariesByRoom(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<bool>())).Throws<Exception>();
            var cont = new CommandController(mockManager.Object, new Mock<IHostApplicationLifetime>().Object);

            var res = cont.SetAllBianriesForRoomByEquipmentType(new Guid(), "test", true);

            Assert.IsType<BadRequestObjectResult>(res);
        }

        [Fact]
        public void StopApplication_OkResult()
        {
            var cont = new CommandController(new Mock<ISensorManager>().Object, new Mock<IHostApplicationLifetime>().Object);
            Assert.IsType<OkResult>(cont.StopService());
        }

        [Fact]
        public void StopApplication_ThrowException_BadRequest()
        {
            var mockManager = new Mock<IHostApplicationLifetime>();
            mockManager.Setup(m => m.StopApplication()).Throws<Exception>();
            var cont = new CommandController(new Mock<ISensorManager>().Object, mockManager.Object);

            var res = cont.StopService();

            Assert.IsType<BadRequestObjectResult>(res);
        }
    }
}
