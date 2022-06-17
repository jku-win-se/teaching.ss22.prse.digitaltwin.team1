using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using SmartRoom.DataSimulatorService.Logic;
using SmartRoom.DataSimulatorService.Logic.Contracts;
using System.Threading.Tasks;
using Xunit;

namespace SmartRoom.DataSimulatorService.Tests
{
    public class SimulatorServiceTest
    {
        [Fact]
        public void Ctor_ValidParam_Ok()
        {
            var serv = new SimulatorService(new Mock<ILogger<SimulatorService>>().Object, new Mock<ISensorManager>().Object, new Mock<IHostApplicationLifetime>().Object);
            Assert.NotNull(serv);
        }

        [Fact]
        public async Task ExecuteStopAsync_ValidParam_NoException()
        {
            using (var serv = new SimulatorService(new Mock<ILogger<SimulatorService>>().Object, new Mock<ISensorManager>().Object, new Mock<IHostApplicationLifetime>().Object))
            {
                try
                {
                    await serv.StartAsync(new System.Threading.CancellationToken());
                    await serv.StopAsync(new System.Threading.CancellationToken());
                }
                catch (System.Exception e)
                {
                    Assert.Null(e);
                }
            }
        }
    }
}
