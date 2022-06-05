using SmartRoom.BaseDataService.Models;
using System.Linq;
using Xunit;

namespace SmartRoom.BaseDataService.Tests
{
    public class SpecialRoomModelTest
    {
        //ToDo check Propnames
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

        //ToDo check empty ctor
        [Fact]
        public void SpecialRoomModelForOurFEDev_EmtyCtor_ValidObject()
        {
            Assert.NotNull(new SpecialRoomModelForOurFEDev());
        }


        //ToDo check ctor with room
        //ToDo wo ist der unteschied zu SpecialRoomModelForOurFEDev_EmtyCtor_ValidObject() ?
        [Fact]
        public void SpecialRoom_Ctor_Room_ok()
        {
            var room = new SpecialRoomModelForOurFEDev();
            Assert.NotNull(room);
        }


        //ToDo check GetRoom
        //ToDo zusätlich muss auch getestet werden ob die Werte des Models korrekt übertragen werden (SpecialRoomModelForOurFEDev mit {Id = new Guid(),....})
        [Fact]
        public void SpecialRoom_GetRoom_Ok()
        {
            var room = new SpecialRoomModelForOurFEDev();
            Assert.NotNull(room.GetRoom());
        }

    }
}
