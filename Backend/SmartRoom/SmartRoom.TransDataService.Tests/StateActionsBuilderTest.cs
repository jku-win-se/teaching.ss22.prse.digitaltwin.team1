using Microsoft.Extensions.DependencyInjection;
using Moq;
using SmartRoom.TransDataService.Logic;
using SmartRoom.TransDataService.Logic.Contracts;
using System;
using Xunit;

namespace SmartRoom.TransDataService.Tests
{
    public class StateActionsBuilderTest
    {
        [Fact]
        public void Ctor_ValidParam_Ok()
        {
            var res = new StateActionsBuilder(new Mock<IServiceProvider>().Object);

            Assert.NotNull(res);
        }

        [Fact]
        public void SecurityActions_Builder()
        {
            var prov = new Mock<IServiceProvider>();
            prov.Setup(p => p.GetService(typeof(ISecurityManager))).Returns(new Mock<ISecurityManager>().Object);
            var res = new StateActionsBuilder(prov.Object);

            Assert.IsType<StateActionsBuilder>(res.SecurityActions());
        }

        [Fact]
        public void EnergySavingActions_Builder()
        {
            var prov = new Mock<IServiceProvider>();
            prov.Setup(p => p.GetService(typeof(IEnergySavingManager))).Returns(new Mock<IEnergySavingManager>().Object);
            var res = new StateActionsBuilder(prov.Object);

            Assert.IsType<StateActionsBuilder>(res.EnergySavingActions());
        }

        [Fact]
        public void AirQualityActions_Builder()
        {
            var prov = new Mock<IServiceProvider>();
            prov.Setup(p => p.GetService(typeof(IAirQualityManager))).Returns(new Mock<IAirQualityManager>().Object);
            var res = new StateActionsBuilder(prov.Object);

            Assert.IsType<StateActionsBuilder>(res.AirQualityActions());
        }

        [Fact]
        public void Build_StateActions()
        {
            var prov = new Mock<IServiceProvider>();
            prov.Setup(p => p.GetService(typeof(IAirQualityManager))).Returns(new Mock<IAirQualityManager>().Object);
            var res = new StateActionsBuilder(prov.Object);

            Assert.IsType<StateActions>(res.AirQualityActions().Build());
        }
    }
}
