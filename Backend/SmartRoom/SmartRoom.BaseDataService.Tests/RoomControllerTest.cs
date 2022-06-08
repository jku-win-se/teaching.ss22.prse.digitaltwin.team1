using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartRoom.BaseDataService.Controllers;
using SmartRoom.BaseDataService.Models;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SmartRoom.BaseDataService.Tests
{
    public class RoomControllerTest
    {
        [Fact]
        public void Ctor_ManagerParam_Ok()
        {
            var mockManager = new Mock<IGenericEntityManager<Room>>();
            Assert.NotNull(new RoomController(mockManager.Object));
        }

        [Fact]
        public async Task GetModels_ValidResult()
        {
            var mockManager = new Mock<IGenericEntityManager<Room>>();
            mockManager.Setup(m => m.Get()).ReturnsAsync(GetRoomsTestData());
            var controller = new RoomController(mockManager.Object);
            
            var res = (IEnumerable<SpecialRoomModelForOurFEDev>)((await controller.GetModels()).Result as OkObjectResult)!.Value!;

            Assert.Equal(2, res!.ToList().Count());
        }

        [Fact]
        public async Task GetModels_ThrowException_BadRequest()
        {
            var mockManager = new Mock<IGenericEntityManager<Room>>();
            mockManager.Setup(m => m.Get()).Throws<Exception>();
           var controller = new RoomController(mockManager.Object);

            var res = (await controller.GetModels()).Result;

            Assert.IsType<BadRequestObjectResult>(res);
        }

        [Fact]
        public async Task PostModel_ValidModel_Ok()
        {
            var model = new SpecialRoomModelForOurFEDev();
            var mockManager = new Mock<IGenericEntityManager<Room>>();
            var controller = new RoomController(mockManager.Object);

            Assert.IsType<OkResult>(await controller.PostModel(model));
        }

        [Fact]
        public async Task PostModel_Null_BadRequest()
        {
            var mockManager = new Mock<IGenericEntityManager<Room>>();
            var controller = new RoomController(mockManager.Object);

            Assert.IsType<BadRequestObjectResult>(await controller.PostModel(null!));
        }

        [Fact]
        public async Task PostModel_ThrowException_BadRequest()
        {
            var mockManager = new Mock<IGenericEntityManager<Room>>();
            mockManager.Setup(m => m.Add(It.IsAny<Room>())).Throws<Exception>();
            var controller = new RoomController(mockManager.Object);

            var res = await controller.PostModel(new SpecialRoomModelForOurFEDev());

            Assert.IsType<BadRequestObjectResult>(res);
        }

        [Fact]
        public async Task PutModel_ValidModel_Ok()
        {
            var model = new SpecialRoomModelForOurFEDev 
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                RoomEquipmentDict = new Dictionary<string, int> 
                {
                    {"Test", 3}
                }
            };
            var modelRemove = new SpecialRoomModelForOurFEDev
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                RoomEquipmentDict = new Dictionary<string, int>
                {
                    {"Test", 1}
                }
            };
            var mockManager = new Mock<IGenericEntityManager<Room>>();
            mockManager.Setup(m => m.GetBy(It.IsAny<Guid>())).ReturnsAsync(GetRoomsTestData().Where(r => r.Id.Equals(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"))).First());

            var controller = new RoomController(mockManager.Object);

            Assert.IsType<OkResult>(await controller.PutModel(model));
            Assert.IsType<OkResult>(await controller.PutModel(modelRemove));
        }

        [Fact]
        public async Task PutModel_Null_BadRequest()
        {
            var mockManager = new Mock<IGenericEntityManager<Room>>();
            var controller = new RoomController(mockManager.Object);

            Assert.IsType<BadRequestObjectResult>(await controller.PutModel(null!));
        }

        [Fact]
        public async Task PutModel_ThrowException_BadRequest()
        {
            var mockManager = new Mock<IGenericEntityManager<Room>>();
            mockManager.Setup(m => m.Update(It.IsAny<Room>())).Throws<Exception>();
            var controller = new RoomController(mockManager.Object);

            var res = await controller.PutModel(new SpecialRoomModelForOurFEDev());

            Assert.IsType<BadRequestObjectResult>(res);
        }

        private Room[] GetRoomsTestData()
        {
            return new List<Room>
            {
                new Room
                {
                    Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6")
                },
                new Room 
                {
                    Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8")
                }
            }.ToArray();
        }
    }
}
