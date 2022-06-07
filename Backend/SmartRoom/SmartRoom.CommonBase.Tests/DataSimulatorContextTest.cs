using Moq;
using SmartRoom.CommonBase.Transfer;
using SmartRoom.CommonBase.Transfer.Contracts;
using System.Threading.Tasks;
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

        [Fact]
        public async Task AddMeasureStates_ValidParams_ValidResult()
        {
            var mock = new Mock<IDataSimulatorContext>();

            try
            {
                await mock.Object.SetAllBinariesForRoomByEqipmentType(new System.Guid(), "", true);
            }
            catch (System.Exception)
            {
                Assert.True(false);
            }
        }
    }
}
