using Microsoft.EntityFrameworkCore;
using Moq;
using SmartRoom.CommonBase.Persistence;
using Xunit;

namespace SmartRoom.CommonBase.Tests
{
    public class UnitOfWorkTest
    {
        [Fact]
        public void Ctor_ValidParam_ValidObject()
        {
            var mock = new Mock<DbContext>();

            var uow = new UnitOfWork(mock.Object);

            Assert.NotNull(uow);
        }

        [Fact]
        public void Using_ValidParam_ValidObject()
        {
            var mock = new Mock<DbContext>();

            using (var uow = new UnitOfWork(mock.Object))
            {
                Assert.NotNull(uow);
            }
        }
    }
}
