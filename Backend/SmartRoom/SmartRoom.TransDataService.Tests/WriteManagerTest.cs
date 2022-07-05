using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Moq;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.TransDataService.Logic;
using SmartRoom.TransDataService.Logic.Contracts;
using SmartRoom.TransDataService.Persistence;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SmartRoom.TransDataService.Tests
{
    public class WriteManagerTest
    {
        [Fact]
        public void Ctor_ValidParam_Ok()
        {
            var manager = new WriteManager(new Mock<IDbContextFactory<TransDataDBContext>>().Object, new Mock<IHubContext<SensorHub>>().Object, new Mock<IStateActions>().Object);
            Assert.NotNull(manager);
        }

        [Fact]
        public async Task AddState_ValidParam_NoException()
        {
            var dbc = new Mock<IDbContextFactory<TransDataDBContext>>();
            var hub = new Mock<IHubContext<SensorHub>>();
            var hubClients = new Mock<IHubClients>();
            hubClients.Setup(hc => hc.All).Returns(new Mock<IClientProxy>().Object);
            hub.Setup(h => h.Clients).Returns(hubClients.Object);
            dbc.Setup(c => c.CreateDbContextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(GetInMemoryDBContext().GetAwaiter().GetResult());

            var manager = new WriteManager(dbc.Object, hub.Object ,new Mock<IStateActions>().Object);

            try
            {
                await manager.addState(new BinaryState[] { new BinaryState() });
            }
            catch (System.Exception e)
            {
                Assert.Null(e);
            }
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
