using Microsoft.EntityFrameworkCore;
using SmartRoom.BaseDataService.Persistence;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Persistence;
using SmartRoom.CommonBase.Persistence.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SmartRoom.BaseDataService.Tests
{
    public class PersistenceTest
    {

        [Fact]
        public void GetRepo_ExistingType_ConcreteRepositories_AndExistingTypes()
        {
            SmartRoomUOW uow = GetInMemoryUOW();
            var roomEquipmentRepo = uow.GetRepo<IGenericEntityRepository<RoomEquipment>>();
            var roomRepo = uow.GetRepo<IGenericEntityRepository<Room>>();

            Assert.IsType<GenericEntityRepository<RoomEquipment>>(roomEquipmentRepo);
            Assert.IsType<RoomRepository>(roomRepo);
        }

        [Fact]
        public async Task GetIncludes_RoomWithEquipment_CorrectCountOfRoomEquipment()
        {
            SmartRoomUOW uow = GetInMemoryUOW();
            var roomRepo = uow.GetRepo<IGenericEntityRepository<Room>>();
            await roomRepo?.Add(new Room
            {
                RoomEquipment = new List<RoomEquipment> { new RoomEquipment() }
            })!;

            await uow.SaveChangesAsync();
            Assert.Equal(1, roomRepo?.Get().GetAwaiter().GetResult().First().RoomEquipment.Count);
        }

        private SmartRoomUOW GetInMemoryUOW()
        {
            DbContextOptions<SmartRoomDBContext> options;
            var builder = new DbContextOptionsBuilder<SmartRoomDBContext>();
            builder.UseInMemoryDatabase("TestDB");
            options = builder.Options;
            SmartRoomDBContext context = new SmartRoomDBContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return new SmartRoomUOW(context);
        }
    }
}