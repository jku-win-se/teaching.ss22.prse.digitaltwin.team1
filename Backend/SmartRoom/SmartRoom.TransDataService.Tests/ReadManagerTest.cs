using Microsoft.EntityFrameworkCore;
using Moq;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.TransDataService.Logic;
using SmartRoom.TransDataService.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SmartRoom.TransDataService.Tests
{
    public class ReadManagerTest
    {
        [Fact]
        public void Ctor_ValidParam_Ok()
        {
            var cont = new ReadManager(new Mock<IDbContextFactory<TransDataDBContext>>().Object);
            Assert.NotNull(cont);
        }

        [Fact]
        public async Task GetStatesByEntityID_ValidIdNotExists_EmptyArray()
        {
            var dbc = new Mock<IDbContextFactory<TransDataDBContext>>();
            dbc.Setup(c => c.CreateDbContextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(GetInMemoryDBContext().GetAwaiter().GetResult());

            var manager = new ReadManager(dbc.Object);
            var res = await manager.GetStatesByEntityID<BinaryState>(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"));

            Assert.NotNull(res);
        }

        [Fact]
        public async Task GetRecentStateByEntityID_ValidIdNotExists_DefaultState()
        {
            var dbcMock = new Mock<IDbContextFactory<TransDataDBContext>>();
            var dbc = await GetInMemoryDBContext();
            dbcMock.Setup(c => c.CreateDbContextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(dbc);

            var manager = new ReadManager(dbcMock.Object);
            var res = await manager.GetRecentStateByEntityID<BinaryState>(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), string.Empty);

            Assert.Equal(res.TimeStamp, DateTime.MinValue);
        }

        [Fact]
        public async Task GetRecentStateByEntityID_ValidIdExists_ExpectetState()
        {
            var dbcMock = new Mock<IDbContextFactory<TransDataDBContext>>();
            var dbc = await GetInMemoryDBContext();
            dbcMock.Setup(c => c.CreateDbContextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(dbc);
            dbc.AddRange(new BinaryState[]
            {
                new BinaryState{ EntityRefID = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), TimeStamp = DateTime.Now, Name = string.Empty},
                new BinaryState{ EntityRefID = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), TimeStamp = DateTime.Now, Name = string.Empty}
            });
            dbc.SaveChanges();

            var manager = new ReadManager(dbcMock.Object);
            var res = await manager.GetRecentStateByEntityID<BinaryState>(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), string.Empty);

            Assert.NotEqual(res.TimeStamp, DateTime.MinValue);
        }

        [Fact]
        public async Task GetStateTypesByEntityID_ValidId_EmptyArray()
        {
            var dbc = new Mock<IDbContextFactory<TransDataDBContext>>();
            dbc.Setup(c => c.CreateDbContextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(GetInMemoryDBContext().GetAwaiter().GetResult());

            var manager = new ReadManager(dbc.Object);
            var res = await manager.GetStateTypesByEntityID<BinaryState>(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"));

            Assert.NotNull(res);
        }

        private async Task<TransDataDBContext> GetInMemoryDBContext()
        {
            DbContextOptions<TransDataDBContext> options;
            var builder = new DbContextOptionsBuilder<TransDataDBContext>();
            builder.UseInMemoryDatabase("TestDB");
            options = builder.Options;
            TransDataDBContext context = new TransDataDBContext(options);

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            return context;
        }
    }
}
