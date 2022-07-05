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

        [Fact]
        public void MeasureChangeState_NoTimeStamp_TimestampNotDefault()
        {
            var sensor = new MeasureSensor(StateUpdated!, new CommonBase.Core.Entities.MeasureState());

            sensor.ChangeState();

            Assert.NotEqual(DateTime.MinValue, sensor.State.TimeStamp);
        }

        [Fact]
        public void BinaryChangeState_NoTimeStamp_TimestampNotDefault()
        {
            var sensor = new BinarySensor(StateUpdated!, new CommonBase.Core.Entities.BinaryState());

            sensor.ChangeState();

            Assert.NotEqual(DateTime.MinValue, sensor.State.TimeStamp);
        }

        [Fact]
        public void BinaryChangeState_WithTimeStamp_TimestampNotDefault()
        {
            var sensor = new BinarySensor(StateUpdated!, new CommonBase.Core.Entities.BinaryState());
            var timeStamp = DateTime.Parse("01.01.2022");
            sensor.ChangeState(timeStamp);

            Assert.Equal(timeStamp, sensor.State.TimeStamp);

            sensor.ChangeState(true, timeStamp);

            Assert.Equal(timeStamp, sensor.State.TimeStamp);
        }

        [Fact]
        public void MeasureChangeState_WithTimeStamp_TimestampNotDefault()
        {
            var sensor = new MeasureSensor(StateUpdated!, new CommonBase.Core.Entities.MeasureState());
            var timeStamp = DateTime.Parse("01.01.2022");

            sensor.ChangeState(timeStamp);

            Assert.Equal(timeStamp, sensor.State.TimeStamp);
        }

        [Fact]
        public void MeasureToString_ExpectedString()
        {
            var sensor = new MeasureSensor(StateUpdated!, new CommonBase.Core.Entities.MeasureState
            {
                EntityRefID = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Name = "Test",
                Value = 10
            });

            var expString = "[Sensor: 3fa85f64-5717-4562-b3fc-2c963f66afa6] [Test: 10]";

            Assert.Equal(expString, sensor.ToString());
        }

        [Fact]
        public void BinaryToString_ExpectedString()
        {
            var sensor = new BinarySensor(StateUpdated!, new CommonBase.Core.Entities.BinaryState
            {
                EntityRefID = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Name = "Test",
                Value = true
            });

            var expString = "[Sensor: 3fa85f64-5717-4562-b3fc-2c963f66afa6] [Test: True]";

            Assert.Equal(expString, sensor.ToString());
        }

        [Fact]
        public void BianryOnStateUpdate_Raises_ExpectedVal()
        {
            var sensor = new BinarySensor(StateUpdated!, new CommonBase.Core.Entities.BinaryState
            {
                EntityRefID = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Name = "Test",
                Value = true
            });

            sensor.ChangeState(true);

            Assert.False(sensor.State.Value);
        }

        [Fact]
        public void MeasureOnStateUpdate_Raises_ExpectedVal()
        {
            var sensor = new MeasureSensor(StateUpdated!, new CommonBase.Core.Entities.MeasureState
            {
                EntityRefID = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Name = "Test",
                Value = 10
            });

            sensor.ChangeState();

            Assert.Equal(0, sensor.State.Value);
        }

        private void StateUpdated(object sender, EventArgs e)
        {
            if(sender is BinarySensor) (sender as BinarySensor)!.State.Value = false;
            if(sender is MeasureSensor) (sender as MeasureSensor)!.State.Value = 0;
        }
    }
}
