using SmartRoom.DataSimulatorService.Models;
using System;
using Xunit;

namespace SmartRoom.DataSimulatorService.Tests
{
    public class SensorTest
    {
        [Fact]
        public void BinaryCtor_ManagerParam_Ok()
        {       
            Assert.NotNull(new BinarySensor(StateUpdated!, new CommonBase.Core.Entities.BinaryState()));
        }

        [Fact]
        public void MeasureCtor_ManagerParam_Ok()
        {
            Assert.NotNull(new MeasureSensor(StateUpdated!, new CommonBase.Core.Entities.MeasureState()));
        }

        private void StateUpdated(object sender, EventArgs e)
        {

        }
    }
}
