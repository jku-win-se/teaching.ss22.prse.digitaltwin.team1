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
        //ToDo check ctor with room
        //ToDo check GetRoom
    }
}
