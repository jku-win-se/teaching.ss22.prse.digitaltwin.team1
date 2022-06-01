using Moq;
using SmartRoom.BaseDataService.Controllers;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Logic.Contracts;
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
    }
}
