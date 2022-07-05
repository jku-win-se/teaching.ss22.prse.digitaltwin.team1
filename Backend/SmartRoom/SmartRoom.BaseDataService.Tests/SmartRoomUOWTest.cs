using SmartRoom.BaseDataService.Persistence;
using Xunit;

namespace SmartRoom.BaseDataService.Tests
{
    public  class SmartRoomUOWTest
    {
        [Fact]
        public void Ctor_ValidParam_NotNull()
        {
            var context = new SmartRoomDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<SmartRoomDBContext>());

            var obj = new SmartRoomUOW(context);

            Assert.NotNull(obj);
        }

        [Fact]
        public void DbSets_NotNull()
        {
            var context = new SmartRoomDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<SmartRoomDBContext>());

            var obj = new SmartRoomUOW(context);

            Assert.NotNull(obj.RoomEquipments);
            Assert.NotNull(obj.Rooms);
        }
    }
}
