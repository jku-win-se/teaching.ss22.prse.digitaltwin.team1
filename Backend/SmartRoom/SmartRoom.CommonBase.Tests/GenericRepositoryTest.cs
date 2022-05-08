using Microsoft.EntityFrameworkCore;
using SmartRoom.CommonBase.Persistence.Contracts;
using SmartRoom.CommonBase.Tests.Models;
using SmartRoom.CommonBase.Persistence;
using Xunit;
using System.Threading.Tasks;

namespace SmartRoom.CommonBase.Tests
{
    public class GenericRepositoryTest
    {

        [Fact]
        public void GetRepo_ExistingType_ConcreteRepository()
        {
            IUnitOfWork uow = GetInMemoryUOW();
            var repo = uow.GetRepo<IGenericEntityRepository<TestEntity>>();

            Assert.IsType<GenericEntityRepository<TestEntity>>(repo);
        }

        [Fact]
        public async Task Add_ValidItem_ValidLength()
        {
            IUnitOfWork uow = GetInMemoryUOW();
            var repo = uow.GetRepo<IGenericEntityRepository<TestEntity>>();

            TestEntity e = new();

            await repo?.Add(e)!;
            await uow.SaveChangesAsync();

            Assert.Equal(1, repo?.Get().GetAwaiter().GetResult().Length);
        }

        [Fact]
        public async Task GetBy_ValidItem_GetItem()
        {
            IUnitOfWork uow = GetInMemoryUOW();
            var repo = uow.GetRepo<IGenericEntityRepository<TestEntity>>();

            TestEntity e = new TestEntity()
            {
                Id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa4")
            };

            await repo?.Add(e)!;
            await uow.SaveChangesAsync();

            Assert.Equal(e, repo?.GetBy(e.Id).GetAwaiter().GetResult());
        }

        [Fact]
        public async Task Update_ValidItem_GetItemWithChanges()
        {
            IUnitOfWork uow = GetInMemoryUOW();
            var repo = uow.GetRepo<IGenericEntityRepository<TestEntity>>();

            TestEntity e = new TestEntity()
            {
                Id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa4")
            };

            await repo?.Add(e)!;
            await uow.SaveChangesAsync();

            e.TestDate = System.DateTime.Now;
            await repo?.Update(e)!;
            await uow.SaveChangesAsync();

            Assert.Equal(e, repo?.GetBy(e.Id).GetAwaiter().GetResult());
        }

        [Fact]
        public async Task Delete_ExsistingItem_CorrectLength()
        {
            IUnitOfWork uow = GetInMemoryUOW();
            var repo = uow.GetRepo<IGenericEntityRepository<TestEntity>>();

            TestEntity e = new TestEntity()
            {
                Id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa4")
            };

            await repo?.Add(e)!;
            await repo?.Add(new TestEntity())!;
            await uow.SaveChangesAsync();

            await repo?.Delete(await repo?.GetBy(e.Id)!)!;
            await uow.SaveChangesAsync();

            Assert.Equal(1, repo?.Get().GetAwaiter().GetResult().Length);
        }

        private IUnitOfWork GetInMemoryUOW()
        {
            DbContextOptions<TestDBContext> options;
            var builder = new DbContextOptionsBuilder<TestDBContext>();
            builder.UseInMemoryDatabase("TestDB");
            options = builder.Options;
            TestDBContext context = new TestDBContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return new TestUOW(context);
        }

        public class TestDBContext : DbContext
        {
            public TestDBContext(DbContextOptions options) : base(options)
            {
            }
            DbSet<TestEntity>? TestEntities { get; set; }
        }

        public class TestUOW : UnitOfWork
        {
            public TestUOW(TestDBContext context) : base(context)
            {
                TestEntities = new GenericEntityRepository<TestEntity>(context);
            }
            public IGenericEntityRepository<TestEntity> TestEntities { get; private set; }
        }
    }
}
