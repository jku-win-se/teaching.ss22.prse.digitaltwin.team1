using Microsoft.Extensions.Configuration;
using Moq;
using SmartRoom.CommonBase.Transfer;
using SmartRoom.CommonBase.Transfer.Contracts;
using System;
using Xunit;

namespace SmartRoom.CommonBase.Tests
{
    public class ServiceRoutesManagerTest
    {
        [Fact]
        public void TransDataService_ValidResult()
        { 
            Mock<IServiceRoutesManager> mock = new Mock<IServiceRoutesManager>();

            mock.Setup(m => m.TransDataService).Returns("");

            Assert.NotNull(mock.Object.TransDataService);
        }

        [Fact]
        public void TransDataService_Null_ThrowsException()
        {
            Mock<IConfiguration> mock = new Mock<IConfiguration>();

            var manager = new ServiceRoutesManager(mock.Object);

            Assert.Throws<ArgumentNullException>(() => manager.TransDataService);
        }

        [Fact]
        public void BaseDataService_ValidResult()
        {
            Mock<IServiceRoutesManager> mock = new Mock<IServiceRoutesManager>();

            mock.Setup(m => m.BaseDataService).Returns("");

            Assert.NotNull(mock.Object.BaseDataService);
        }

        [Fact]
        public void BaseDataService_Null_ThrowsException()
        {
            Mock<IConfiguration> mock = new Mock<IConfiguration>();

            var manager = new ServiceRoutesManager(mock.Object);

            Assert.Throws<ArgumentNullException>(() => manager.BaseDataService);
        }

        [Fact]
        public void DataSimulatorService_ValidResult()
        {
            Mock<IServiceRoutesManager> mock = new Mock<IServiceRoutesManager>();

            mock.Setup(m => m.DataSimulatorService).Returns("");

            Assert.NotNull(mock.Object.DataSimulatorService);
        }

        [Fact]
        public void DataSimulatorService_Null_ThrowsException()
        {
            Mock<IConfiguration> mock = new Mock<IConfiguration>();

            var manager = new ServiceRoutesManager(mock.Object);

            Assert.Throws<ArgumentNullException>(() => manager.DataSimulatorService);
        }

        [Fact]
        public void ApiKey_ValidResult()
        {
            Mock<IServiceRoutesManager> mock = new Mock<IServiceRoutesManager>();

            mock.Setup(m => m.ApiKey).Returns("");

            Assert.NotNull(mock.Object.ApiKey);
        }

        [Fact]
        public void ApiKey_Null_ThrowsException()
        {
            Mock<IConfiguration> mock = new Mock<IConfiguration>();

            var manager = new ServiceRoutesManager(mock.Object);

            Assert.Throws<ArgumentNullException>(() => manager.ApiKey);
        }
    }
}
