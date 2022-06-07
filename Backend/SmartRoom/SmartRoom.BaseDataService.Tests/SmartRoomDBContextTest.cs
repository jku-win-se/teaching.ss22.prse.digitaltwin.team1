using SmartRoom.BaseDataService.Persistence;
using Xunit;

namespace SmartRoom.BaseDataService.Tests
{
    public class SmartRoomDBCOntextTest
    {
        [Fact]
        public void Ctor_ValidParam_NotNull()
        {
            var context = new SmartRoomDBContext(new Microsoft.EntityFrameworkCore.DbContextOptions<SmartRoomDBContext>());

            Assert.NotNull(context.Rooms);
            Assert.NotNull(context.RoomEquipments);
        }
    }
}
