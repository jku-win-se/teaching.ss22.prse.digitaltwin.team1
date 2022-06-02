using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.TransDataService.Controllers;
using SmartRoom.TransDataService.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SmartRoom.TransDataService.Tests
{
    public class TransReadControllerTest
    {
        [Fact]
        public void BinaryCtor_ValidParam_Ok()
        {
            var cont = new ReadBinaryController(new Mock<IReadManager>().Object);
            Assert.NotNull(cont);
        }

        [Fact]
        public void MeasureCtor_ValidParam_Ok()
        {
            var cont = new ReadMeasureController(new Mock<IReadManager>().Object);
            Assert.NotNull(cont);
        }

        [Fact]
        public async Task GetByEntityId_ValidEntityId_ValidResult()
        {
            var id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var mockManager = new Mock<IReadManager>();
            mockManager.Setup(m => m.GetStatesByEntityID<BinaryState>(It.IsAny<Guid>())).ReturnsAsync(GetTestStates().Where(s => s.Id.Equals(id)).ToArray());

            var controller = new ReadBinaryController(mockManager.Object);

            var res = (IEnumerable<BinaryState>)((await controller.GetBy(id)).Result as OkObjectResult)!.Value!;

            Assert.Single(res!);
        }

        [Fact]
        public async Task GetRecent_ValidParams_ValidResult()
        {
            var id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var mockManager = new Mock<IReadManager>();
            mockManager.Setup(m => m.GetRecentStateByEntityID<BinaryState>(It.IsAny<Guid>(), It.IsAny<string>()))!.ReturnsAsync(GetTestStates().FirstOrDefault(s => s.Id.Equals(id) && s.Name.Equals("isTriggerd")));

            var controller = new ReadBinaryController(mockManager.Object);

            var res = (BinaryState) ((await controller.GetRecentBy(id, "isTriggerd")).Result as OkObjectResult)!.Value!;

            Assert.NotNull(res);
        }

        [Fact]
        public async Task GetStateTypes_ValidParams_ValidResult()
        {
            var id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var mockManager = new Mock<IReadManager>();
            mockManager.Setup(m => m.GetStateTypesByEntityID<BinaryState>(It.IsAny<Guid>()))!.ReturnsAsync(new string[] { "isTriggerd"});

            var controller = new ReadBinaryController(mockManager.Object);

            var res = (string[])((await controller.GetTypesBy(id)).Result as OkObjectResult)!.Value!;

            Assert.Single(res!);
        }


        private List<BinaryState> GetTestStates()
        {
            return new BinaryState[]
            {
                new BinaryState
                {
                    Id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                },
                new BinaryState
                {
                    Id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8"),
                }
            }.ToList();
        }
    }
}
