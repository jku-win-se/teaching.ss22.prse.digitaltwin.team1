using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartRoom.CommonBase.Logic.Contracts;
using SmartRoom.CommonBase.Tests.Models;
using SmartRoom.CommonBase.Web;
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
            mockManager.Setup(m => m.Get()).ReturnsAsync(GetTestEntities());

            var controller = new GenericEntityController<TestEntity>(mockManager.Object);

            var res = (IEnumerable<TestEntity>) ((await controller.GetAll()).Result as OkObjectResult)!.Value;

            Assert.Equal(2, res.Count());
        }

        [Fact]
        public async Task GetById_ValidId_ValidResult()
        {
            var mockManager = new Mock<IGenericEntityManager<TestEntity>>();
            var id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");

            mockManager.Setup(m => m.GetBy(id))!.ReturnsAsync(GetTestEntities().FirstOrDefault(s => s.Id == id));

            var controller = new GenericEntityController<TestEntity>(mockManager.Object);

            var res = (TestEntity)((await controller.Get(id)).Result as OkObjectResult)!.Value;

            Assert.Equal(GetTestEntities()[0], res);
        }

        [Fact]
        public async Task Add_ValidEntity_SameEntity()
        {
            var mockManager = new Mock<IGenericEntityManager<TestEntity>>();
            var id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");

            mockManager.Setup(m => m.GetBy(id))!.ReturnsAsync(GetTestEntities().FirstOrDefault(s => s.Id == id));

            var controller = new GenericEntityController<TestEntity>(mockManager.Object);

            var res = (TestEntity)((await controller.Get(id)).Result as OkObjectResult)!.Value;

            Assert.Equal(GetTestEntities()[0], res);
        }

        private TestEntity[] GetTestEntities()
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
            };
        }
    }
}
