using Moq;
using SmartRoom.CommonBase.Transfer;
using SmartRoom.CommonBase.Transfer.Contracts;
using Xunit;

namespace SmartRoom.CommonBase.Tests
{
    public class DataSimulatorContextTest
    {
        [Fact]
        public void Ctor_ValidParam_Ok()
        {
            var mock = new Mock<IServiceRoutesManager>();

            var context = new DataSimulatorContext(mock.Object);

            Assert.NotNull(context);
        }
    }
}
