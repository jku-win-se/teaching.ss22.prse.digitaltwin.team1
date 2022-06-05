using Moq;
using SmartRoom.CommonBase.Transfer;
using SmartRoom.CommonBase.Transfer.Contracts;
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
        public void GetRecentMeasureStateBy_ValidParams_ValidResult()
        {
            var mock = new Mock<IServiceRoutesManager>();
            mock.Setup(m => m.BaseDataService).Returns("https://transdataservice.azurewebsites.net/api/");
            mock.Setup(m => m.ApiKey).Returns("bFR9bGhOi0n0ccoEhrhsE57VrHjkJJz9");
            var context = new TransDataServiceContext(mock.Object);

            Assert.NotNull(context.GetRecentMeasureStateBy(new System.Guid("026bdf16-0d67-43ad-9d5d-afef430f1589"), "Co2"));
        }

        [Fact]
        public void GetRecentBinaryStateBy_ValidParams_ValidResult()
        {
            var mock = new Mock<IServiceRoutesManager>();
            mock.Setup(m => m.BaseDataService).Returns("https://transdataservice.azurewebsites.net/api/");
            mock.Setup(m => m.ApiKey).Returns("bFR9bGhOi0n0ccoEhrhsE57VrHjkJJz9");
            var context = new TransDataServiceContext(mock.Object);

            Assert.NotNull(context.GetRecentBinaryStateBy(new System.Guid("026bdf16-0d67-43ad-9d5d-afef430f1589"), "Co2"));
        }
    }
}
