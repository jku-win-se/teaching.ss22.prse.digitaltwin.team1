using Microsoft.Extensions.Logging;
using Moq;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Transfer.Contracts;
using SmartRoom.DataSimulatorService.Logic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SmartRoom.DataSimulatorService.Tests
{
    public class SensorManagerTest
    {
        [Fact]
        public void Ctor_Ok()
        {
            var manager = new SensorManager(new Mock<ILogger<SensorManager>>().Object, new Mock<ITransDataServiceContext>().Object, new Mock<IBaseDataServiceContext>().Object);
            Assert.NotNull(manager);
        }

        [Fact]
        public async Task Init_NoException()
        {
            var baseDataContext = new Mock<IBaseDataServiceContext>();
            var transDatContext = new Mock<ITransDataServiceContext>();

            baseDataContext.Setup(bd => bd.GetRooms()).ReturnsAsync(new List<Room> { new Room { Id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6") } });
            baseDataContext.Setup(bd => bd.GetRoomEquipments()).ReturnsAsync(new List<RoomEquipment> { new RoomEquipment { Id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8"), Name = "Ventilator" } });

            transDatContext.Setup(td => td.GetRecentMeasureStateBy(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(new MeasureState());
            transDatContext.Setup(td => td.GetRecentBinaryStateBy(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(new BinaryState());

            var manager = new SensorManager(new Mock<ILogger<SensorManager>>().Object, transDatContext.Object, baseDataContext.Object);

            try
            {
                await manager.Init();
            }
            catch (Exception e)
            {
                Assert.Null(e);
            }
        }

        [Fact]
        public async Task GenerateMissingData_NoException()
        {
            var baseDataContext = new Mock<IBaseDataServiceContext>();
            var transDatContext = new Mock<ITransDataServiceContext>();

            baseDataContext.Setup(bd => bd.GetRooms()).ReturnsAsync(new List<Room> { new Room { Id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6") } });
            baseDataContext.Setup(bd => bd.GetRoomEquipments()).ReturnsAsync(new List<RoomEquipment> { new RoomEquipment { Id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8"), Name = "Ventilator" } });

            transDatContext.Setup(td => td.GetRecentMeasureStateBy(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(new MeasureState());
            transDatContext.Setup(td => td.GetRecentBinaryStateBy(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(new BinaryState());

            var manager = new SensorManager(new Mock<ILogger<SensorManager>>().Object, transDatContext.Object, baseDataContext.Object);

            try
            {
                await manager.Init();
                await manager.GenerateMissingData();
            }
            catch (Exception e)
            {
                Assert.Null(e);
            }
        }

        [Fact]
        public async Task ChangeState_Changed()
        {
            var baseDataContext = new Mock<IBaseDataServiceContext>();
            var transDatContext = new Mock<ITransDataServiceContext>();
            var roomE = new RoomEquipment { Id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8"), Name = "Ventilator" };
            baseDataContext.Setup(bd => bd.GetRooms()).ReturnsAsync(new List<Room> { new Room { Id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6") } });
            baseDataContext.Setup(bd => bd.GetRoomEquipments()).ReturnsAsync(new List<RoomEquipment> { roomE });
            transDatContext.Setup(td => td.GetRecentMeasureStateBy(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(new MeasureState());
            transDatContext.Setup(td => td.GetRecentBinaryStateBy(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(new BinaryState());
            var manager = new SensorManager(new Mock<ILogger<SensorManager>>().Object, transDatContext.Object, baseDataContext.Object);

            try
            {
                await manager.Init();
                await manager.GenerateMissingData();
                manager.ChangeState(new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8"), "isTriggerd");
            }
            catch (Exception e)
            {
                Assert.Null(e);
            }
        }

        [Fact]
        public async Task SetAllBinariesByRoom_Changed()
        {
            var baseDataContext = new Mock<IBaseDataServiceContext>();
            var transDatContext = new Mock<ITransDataServiceContext>();
            var roomE = new RoomEquipment { Id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8"), Name = "Ventilator", RoomID = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6") };
            baseDataContext.Setup(bd => bd.GetRooms()).ReturnsAsync(new List<Room> { new Room { Id = new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6") } });
            baseDataContext.Setup(bd => bd.GetRoomEquipments()).ReturnsAsync(new List<RoomEquipment> { roomE });
            transDatContext.Setup(td => td.GetRecentMeasureStateBy(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(new MeasureState());
            transDatContext.Setup(td => td.GetRecentBinaryStateBy(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(new BinaryState());
            var manager = new SensorManager(new Mock<ILogger<SensorManager>>().Object, transDatContext.Object, baseDataContext.Object);

            try
            {
                await manager.Init();
                manager.SetAllBinariesByRoom(new System.Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), "isTriggerd", true);
            }
            catch (Exception e)
            {
                Assert.Null(e);
            }
        }
    }
}
