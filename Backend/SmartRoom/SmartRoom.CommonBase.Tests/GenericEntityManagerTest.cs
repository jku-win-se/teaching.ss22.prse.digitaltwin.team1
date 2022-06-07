using Moq;
using SmartRoom.CommonBase.Logic;
using SmartRoom.CommonBase.Persistence.Contracts;
using SmartRoom.CommonBase.Tests.Models;
using Xunit;

namespace SmartRoom.CommonBase.Tests
{
    public class GenericEntityManagerTest
    {
        [Fact]
        public void Ctor_ValidParam_ValidObject()
        {
            var mock = new Mock<IUnitOfWork>();

            var context = new GenericEntityManager<TestEntity>(mock.Object);

            Assert.NotNull(context);
        }
    }
}
