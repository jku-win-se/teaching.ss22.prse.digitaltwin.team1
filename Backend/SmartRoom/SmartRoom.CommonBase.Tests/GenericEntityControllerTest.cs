using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartRoom.CommonBase.Logic.Contracts;
using SmartRoom.CommonBase.Tests.Models;
using SmartRoom.CommonBase.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SmartRoom.CommonBase.Tests
{
    public class GenericEntityControllerTest
    {
        [Fact]
        public async Task GetAll_ValidResult()
        {
            var mockManager = new Mock<IGenericEntityManager<TestEntity>>();
            mockManager.Setup(m => m.Get()).ReturnsAsync(GetTestEntities().ToArray());

            var controller = new GenericEntityController<TestEntity>(mockManager.Object);

            var res = (IEnumerable<TestEntity>)((await controller.GetAll()).Result as OkObjectResult)!.Value!;

            Assert.Equal(2, res!.Count());
        }

        [Fact]
        public async Task GetAll_ThrowException_BadRequest()
        {
            var mockManager = new Mock<IGenericEntityManager<TestEntity>>();
            mockManager.Setup(m => m.Get()).Throws<Exception>();
            var controller = new GenericEntityController<TestEntity>(mockManager.Object);

            var res = await controller.GetAll();

            Assert.IsType<BadRequestObjectResult>(res.Result);
        }

        [Fact]
        public async Task GetById_ValidId_ValidResult()
        {
            var mockManager = new Mock<IGenericEntityManager<TestEntity>>();
            var id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");

            mockManager.Setup(m => m.GetBy(id))!.ReturnsAsync(GetTestEntities().FirstOrDefault(s => s.Id == id));

            var controller = new GenericEntityController<TestEntity>(mockManager.Object);

            var res = (TestEntity)((await controller.Get(id)).Result as OkObjectResult)!.Value!;

            Assert.Equal(GetTestEntities()[0], res);
        }

        [Fact]
        public async Task GetById_ThrowException_BadRequest()
        {
            var mockManager = new Mock<IGenericEntityManager<TestEntity>>();
            mockManager.Setup(m => m.GetBy(It.IsAny<Guid>())).Throws<Exception>();
            var controller = new GenericEntityController<TestEntity>(mockManager.Object);

            var res = await controller.Get(new Guid());

            Assert.IsType<BadRequestObjectResult>(res.Result);
        }

        [Fact]
        public async Task Add_ValidEntity_OkResult()
        {
            var mockManager = new Mock<IGenericEntityManager<TestEntity>>();
            var e = new TestEntity
            {
                Id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa4")
            };

            var controller = new GenericEntityController<TestEntity>(mockManager.Object);
            mockManager.Setup(m => m.Add(e))!.Returns(Task.CompletedTask).Verifiable();

            var res = await controller.Post(e);

            Assert.IsType<OkResult>(res);
        }

        [Fact]
        public async Task Add_Null_BadRequest()
        {
            var mockManager = new Mock<IGenericEntityManager<TestEntity>>();

            var controller = new GenericEntityController<TestEntity>(mockManager.Object);
            mockManager.Setup(m => m.Add(null!)).Returns(Task.CompletedTask).Verifiable();

            var res = await controller.Post(null!);

            Assert.IsType<BadRequestObjectResult>(res);
        }

        [Fact]
        public async Task Add_ThrowException_BadRequest()
        {
            var mockManager = new Mock<IGenericEntityManager<TestEntity>>();
            mockManager.Setup(m => m.Add(It.IsAny<TestEntity>())).Throws<Exception>();
            var controller = new GenericEntityController<TestEntity>(mockManager.Object);

            var res = await controller.Post(new TestEntity());

            Assert.IsType<BadRequestObjectResult>(res);
        }

        [Fact]
        public async Task Update_ValidEntity_OkResult()
        {
            var mockManager = new Mock<IGenericEntityManager<TestEntity>>();
            var e = new TestEntity
            {
                Id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa4")
            };

            var controller = new GenericEntityController<TestEntity>(mockManager.Object);
            mockManager.Setup(m => m.Update(e))!.Returns(Task.CompletedTask).Verifiable();

            var res = await controller.Put(e);

            Assert.IsType<OkResult>(res);
        }
        [Fact]
        public async Task Update_Null_BadRequest()
        {
            var mockManager = new Mock<IGenericEntityManager<TestEntity>>();

            var controller = new GenericEntityController<TestEntity>(mockManager.Object);
            mockManager.Setup(m => m.Update(null!)).Returns(Task.CompletedTask).Verifiable();

            var res = await controller.Put(null!);

            Assert.IsType<BadRequestObjectResult>(res);
        }

        [Fact]
        public async Task Update_ThrowException_BadRequest()
        {
            var mockManager = new Mock<IGenericEntityManager<TestEntity>>();
            mockManager.Setup(m => m.Update(It.IsAny<TestEntity>())).Throws<Exception>();
            var controller = new GenericEntityController<TestEntity>(mockManager.Object);

            var res = await controller.Put(new TestEntity());

            Assert.IsType<BadRequestObjectResult>(res);
        }

        [Fact]
        public async Task Delete_ValidGuid_OkResult()
        {
            var mockManager = new Mock<IGenericEntityManager<TestEntity>>();
            var id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa4");
            var controller = new GenericEntityController<TestEntity>(mockManager.Object);

            mockManager.Setup(m => m.Delete(id))!.Returns(Task.CompletedTask).Verifiable();

            var res = await controller.Delete(id);

            Assert.IsType<OkResult>(res);
        }

        [Fact]
        public async Task Delete_ThrowException_BadRequest()
        {
            var mockManager = new Mock<IGenericEntityManager<TestEntity>>();
            mockManager.Setup(m => m.Delete(It.IsAny<Guid>())).Throws<Exception>();
            var controller = new GenericEntityController<TestEntity>(mockManager.Object);

            var res = await controller.Delete(new Guid());

            Assert.IsType<BadRequestObjectResult>(res);
        }

        private List<TestEntity> GetTestEntities()
        {
            return new TestEntity[]
            {
                new TestEntity
                {
                    Id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                },
                new TestEntity
                {
                    Id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8"),
                }
            }.ToList();
        }
    }
}
