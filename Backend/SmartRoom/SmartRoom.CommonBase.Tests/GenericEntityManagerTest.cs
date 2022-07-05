using Moq;
using SmartRoom.CommonBase.Logic;
using SmartRoom.CommonBase.Persistence.Contracts;
using SmartRoom.CommonBase.Tests.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        [Fact]
        public async Task Get_Ok()
        {
            var mock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericEntityRepository<TestEntity>>();
            repoMock.Setup(m => m.Get()).ReturnsAsync(new TestEntity[] { new TestEntity() });
            mock.Setup(m => m.GetRepo<IGenericEntityRepository<TestEntity>>()).Returns(repoMock.Object);

            var context = new GenericEntityManager<TestEntity>(mock.Object);

            Assert.Single(await context.Get());
        }

        [Fact]
        public async Task GetBy_ExisitingId_Ok()
        {
            var mock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericEntityRepository<TestEntity>>();
            repoMock.Setup(m => m.GetBy(It.IsAny<Guid>())).ReturnsAsync(new TestEntity { Id =  new System.Guid("026bdf16-0d67-43ad-9d5d-afef430f1589") });
            mock.Setup(m => m.GetRepo<IGenericEntityRepository<TestEntity>>()).Returns(repoMock.Object);

            var context = new GenericEntityManager<TestEntity>(mock.Object);

            Assert.NotNull(await context.GetBy(new System.Guid("026bdf16-0d67-43ad-9d5d-afef430f1589")));
        }

        [Fact]
        public async Task Add_ValidParam_Ok()
        {
            var mock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericEntityRepository<TestEntity>>();
            mock.Setup(m => m.GetRepo<IGenericEntityRepository<TestEntity>>()).Returns(repoMock.Object);
            
            var context = new GenericEntityManager<TestEntity>(mock.Object);

            try
            {
                await context.Add(new TestEntity());
            }
            catch (System.Exception e)
            {
                Assert.Null(e);
            }
        }

        [Fact]
        public void Add_Null_ArgumentNullException()
        {
            var mock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericEntityRepository<TestEntity>>();
            mock.Setup(m => m.GetRepo<IGenericEntityRepository<TestEntity>>()).Returns(repoMock.Object);

            var context = new GenericEntityManager<TestEntity>(mock.Object);

            Assert.Throws<ArgumentNullException>(() => context.Add(null!).GetAwaiter().GetResult());
        }

        [Fact]
        public async Task Delete_ValidParam_Ok()
        {
            var mock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericEntityRepository<TestEntity>>();

            repoMock.Setup(m => m.Delete(It.IsAny<TestEntity>()));
            repoMock.Setup(m => m.GetBy(It.IsAny<Guid>())).ReturnsAsync(new TestEntity { Id = new System.Guid("026bdf16-0d67-43ad-9d5d-afef430f1589") });
            mock.Setup(m => m.GetRepo<IGenericEntityRepository<TestEntity>>()).Returns(repoMock.Object);

            var context = new GenericEntityManager<TestEntity>(mock.Object);

            try
            {
                await context.Delete(new System.Guid("026bdf16-0d67-43ad-9d5d-afef430f1589"));
            }
            catch (System.Exception e)
            {
                Assert.Null(e);
            }
        }

        [Fact]
        public void Delete_NotExistingKey_KeyNotFoundException()
        {
            var mock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericEntityRepository<TestEntity>>();
            repoMock.Setup(m => m.Delete(It.IsAny<TestEntity>()));
            mock.Setup(m => m.GetRepo<IGenericEntityRepository<TestEntity>>()).Returns(repoMock.Object);

            var context = new GenericEntityManager<TestEntity>(mock.Object);
            
            Assert.Throws<KeyNotFoundException>(() => context.Delete(new Guid()).GetAwaiter().GetResult());
        }

        [Fact]
        public async Task Update_ValidParam_Ok()
        {
            var mock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericEntityRepository<TestEntity>>();
            repoMock.Setup(m => m.Delete(It.IsAny<TestEntity>()));
            repoMock.Setup(m => m.GetBy(It.IsAny<Guid>())).ReturnsAsync(new TestEntity { Id = new System.Guid("026bdf16-0d67-43ad-9d5d-afef430f1589") });
            mock.Setup(m => m.GetRepo<IGenericEntityRepository<TestEntity>>()).Returns(repoMock.Object);

            var context = new GenericEntityManager<TestEntity>(mock.Object);

            try
            {
                await context.Update(new TestEntity());
            }
            catch (System.Exception e)
            {
                Assert.Null(e);
            }
        }

        [Fact]
        public void Update_Null_ArgumentNullException()
        {
            var mock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericEntityRepository<TestEntity>>();
            repoMock.Setup(m => m.Delete(It.IsAny<TestEntity>()));
            mock.Setup(m => m.GetRepo<IGenericEntityRepository<TestEntity>>()).Returns(repoMock.Object);

            var context = new GenericEntityManager<TestEntity>(mock.Object);

            Assert.Throws<ArgumentNullException>(() => context.Update(null!).GetAwaiter().GetResult());
        }

        [Fact]
        public void Update_NotExisitngObject_ArgumentNullException()
        {
            var mock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IGenericEntityRepository<TestEntity>>();
            repoMock.Setup(m => m.Update(It.IsAny<TestEntity>()));
            mock.Setup(m => m.GetRepo<IGenericEntityRepository<TestEntity>>()).Returns(repoMock.Object);

            var context = new GenericEntityManager<TestEntity>(mock.Object);

            Assert.Throws<KeyNotFoundException>(() => context.Update(new TestEntity()).GetAwaiter().GetResult());
        }
    }
}
