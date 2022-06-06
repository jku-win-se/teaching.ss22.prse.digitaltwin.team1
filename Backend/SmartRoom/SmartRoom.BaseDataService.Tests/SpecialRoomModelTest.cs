using SmartRoom.BaseDataService.Models;
using System.Linq;
using Xunit;

namespace SmartRoom.BaseDataService.Tests
{
    public class SpecialRoomModelTest
    {
        [Theory]
        [InlineData("Id")]
        [InlineData("PeopleCount")]
        [InlineData("Name")]
        [InlineData("Size")]
        [InlineData("RoomType")]
        [InlineData("Building")]
        [InlineData("RoomEquipmentDict")]

        public void SpecialRoom_PropertyNames_Exist(string name)
        {
            Assert.NotNull(typeof(SpecialRoomModelForOurFEDev).GetProperties().First(p => p.Name.Equals(name)));
        }

        [Fact]
        public void Ctor_Empty_ValidObject()
        {
            Assert.NotNull(new SpecialRoomModelForOurFEDev());
        }

        //ToDo zusätlich muss auch getestet werden ob die Werte des Models korrekt übertragen werden (SpecialRoomModelForOurFEDev mit {Id = new Guid(),....})
        [Fact]
        public void GetRoom_ValidRoomObj()
        {
            var roomModel = new SpecialRoomModelForOurFEDev
            {
                Name = "nametest",
                Building = "buildingtest",
                Id = new System.Guid("026bdf16-0d67-43ad-9d5d-afef430f1589"),
                RoomEquipmentDict = new System.Collections.Generic.Dictionary<string, int>(),
                PeopleCount = 4,
                RoomType = "labor",
                Size = 20
            };

            var room = roomModel.GetRoom();

            Assert.Equal(room.Id,roomModel.Id);
            Assert.Equal(room.Name,roomModel.Name);
            Assert.Equal(room.Building, roomModel.Building);
            Assert.Equal(room.PeopleCount, roomModel.PeopleCount);
            Assert.Equal(room.RoomType, roomModel.RoomType);
            Assert.Equal(room.Size, roomModel.Size);
        }

    }
}
