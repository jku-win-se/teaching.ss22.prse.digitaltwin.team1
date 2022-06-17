using Moq;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Transfer;
using SmartRoom.CommonBase.Transfer.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SmartRoom.CommonBase.Tests
{
    public class TransDataServiceContextTest
    {
        [Fact]
        public void Ctor_ValidParam_Ok()
        {
            var mock = new Mock<IServiceRoutesManager>();

            var context = new TransDataServiceContext(mock.Object);

            Assert.NotNull(context);
        }

        [Fact]
        public async Task GetRecentMeasureStateBy_ValidParams_ValidResult()
        {
            var mock = new Mock<IServiceRoutesManager>();
            mock.Setup(m => m.TransDataService).Returns("https://transdataservice.azurewebsites.net/api/");
            mock.Setup(m => m.ApiKey).Returns("bFR9bGhOi0n0ccoEhrhsE57VrHjkJJz9");
            var context = new TransDataServiceContext(mock.Object);
            mock.Setup(m => m.BaseDataService).Returns("https://basedataservice.azurewebsites.net/api/");
            var bContext = new BaseDataServiceContext(mock.Object);
        
            Assert.NotNull(await context.GetRecentMeasureStateBy((await bContext.GetRooms()).First().Id, "Co2"));
        }

        //[Fact]
        //public async Task GetRecentBinaryStateBy_ValidParams_ValidResult()
        //{
        //    var mock = new Mock<IServiceRoutesManager>();
        //    mock.Setup(m => m.TransDataService).Returns("https://transdataservice.azurewebsites.net/api/");
        //    mock.Setup(m => m.ApiKey).Returns("bFR9bGhOi0n0ccoEhrhsE57VrHjkJJz9");
        //    var context = new TransDataServiceContext(mock.Object);
        //    mock.Setup(m => m.BaseDataService).Returns("https://basedataservice.azurewebsites.net/api/");
        //    var bContext = new BaseDataServiceContext(mock.Object);
        //
        //    Assert.NotNull(await context.GetRecentBinaryStateBy((await bContext.GetRoomEquipments()).First(r => r.Name == "Light").Id, "IsOn"));
        //}

        [Fact]
        public void AddBinaryStates_ValidParams_ValidResult()
        {
            var mock = new Mock<ITransDataServiceContext>();

            var param = new BinaryState[] { new BinaryState() };

            try
            {
                mock.Object.AddBinaryStates(param);
            }
            catch (System.Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void AddMeasureStates_ValidParams_ValidResult()
        {
            var mock = new Mock<ITransDataServiceContext>();

            var param = new MeasureState[] { new MeasureState() };
            
            try
            {
                mock.Object.AddMeasureStates(param);
            }
            catch (System.Exception)
            {
                Assert.True(false);
            }
        }
    }
}
