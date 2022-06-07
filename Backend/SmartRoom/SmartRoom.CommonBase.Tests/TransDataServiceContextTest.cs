using Moq;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Transfer;
using SmartRoom.CommonBase.Transfer.Contracts;
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

            Assert.NotNull(await context.GetRecentMeasureStateBy(new System.Guid("026bdf16-0d67-43ad-9d5d-afef430f1589"), "Co2"));
        }

        [Fact]
        public async Task GetRecentBinaryStateBy_ValidParams_ValidResult()
        {
            var mock = new Mock<IServiceRoutesManager>();
            mock.Setup(m => m.TransDataService).Returns("https://transdataservice.azurewebsites.net/api/");
            mock.Setup(m => m.ApiKey).Returns("bFR9bGhOi0n0ccoEhrhsE57VrHjkJJz9");
            var context = new TransDataServiceContext(mock.Object);

            Assert.NotNull(await context.GetRecentBinaryStateBy(new System.Guid("026bdf16-0d67-43ad-9d5d-afef430f1589"), "Co2"));
        }

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
