using Moq;
using SmartRoom.CommonBase.Transfer;
using SmartRoom.CommonBase.Transfer.Contracts;
using Xunit;

namespace SmartRoom.CommonBase.Tests
{
    public class BaseDataServiceContextTest
    {
        [Fact]
        public void Ctor_ValidParam_Ok()
        {
            var mock = new Mock<IServiceRoutesManager>();

            var context = new BaseDataServiceContext(mock.Object);

            Assert.NotNull(context);
        }

        [Fact]
        public void GetRooms_ValidResult()
        {
            var mock = new Mock<IServiceRoutesManager>();
            mock.Setup(m => m.BaseDataService).Returns("https://basedataservice.azurewebsites.net/api/");
            mock.Setup(m => m.ApiKey).Returns("bFR9bGhOi0n0ccoEhrhsE57VrHjkJJz9");
            var context = new BaseDataServiceContext(mock.Object);

            Assert.NotNull(context.GetRooms());
        }

        [Fact]
        public void GetRoomEquipments_ValidResult()
        {
            var mock = new Mock<IServiceRoutesManager>();
            mock.Setup(m => m.BaseDataService).Returns("https://basedataservice.azurewebsites.net/api/");
            mock.Setup(m => m.ApiKey).Returns("bFR9bGhOi0n0ccoEhrhsE57VrHjkJJz9");
            var context = new BaseDataServiceContext(mock.Object);

            Assert.NotNull(context.GetRooms());
        }
    }
}
