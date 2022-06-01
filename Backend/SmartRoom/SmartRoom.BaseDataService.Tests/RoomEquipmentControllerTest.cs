using Moq;
using SmartRoom.BaseDataService.Controllers;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Logic.Contracts;
using Xunit;

namespace SmartRoom.BaseDataService.Tests
{
    public class RoomEquipmentControllerTest
    {
        [Fact]
        public void Ctor_ManagerParam_Ok()
        {
            var mockManager = new Mock<IGenericEntityManager<RoomEquipment>>();
            Assert.NotNull(new RoomEquipmentController(mockManager.Object));
        }
    }
}
