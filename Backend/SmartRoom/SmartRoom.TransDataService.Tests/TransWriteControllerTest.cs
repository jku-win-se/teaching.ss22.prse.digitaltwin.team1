using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.TransDataService.Controllers;
using SmartRoom.TransDataService.Logic.Contracts;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SmartRoom.TransDataService.Tests
{
    public  class TransWriteControllerTest
    {
        [Fact]
        public void BinaryCtor_ValidParam_Ok()
        {
            var cont = new TransWriteController(new Mock<IWriteManager>().Object);
            Assert.NotNull(cont);
        }

        [Fact]
        public async Task PostBinary_ValidParam_OkResult()
        {
            var cont = new TransWriteController(new Mock<IWriteManager>().Object);

            Assert.IsType<OkResult>(await cont.AddBinaryState(new BinaryState[] { new BinaryState() }));
        }

        [Fact]
        public async Task PostBinary_ThrowException_BadResult()
        {
            var mock = new Mock<IWriteManager>();
            mock.Setup(m => m.addState(It.IsAny<BinaryState[]>())).Throws<Exception>();

            var cont = new TransWriteController(mock.Object);

            Assert.IsType<BadRequestObjectResult>(await cont.AddBinaryState(new BinaryState[] { new BinaryState() }));
        }

        [Fact]
        public async Task PostMeasure_ValidParam_OkResult()
        {
            var cont = new TransWriteController(new Mock<IWriteManager>().Object);

            Assert.IsType<OkResult>(await cont.AddMeasureState(new MeasureState[] { new MeasureState()}));
        }

        [Fact]
        public async Task PostMeasure_ThrowException_BadResult()
        {
            var mock = new Mock<IWriteManager>();
            mock.Setup(m => m.addState(It.IsAny<MeasureState[]>())).Throws<Exception>();

            var cont = new TransWriteController(mock.Object);

            Assert.IsType<BadRequestObjectResult>(await cont.AddMeasureState(new MeasureState[] { new MeasureState() }));
        }
    }
}
