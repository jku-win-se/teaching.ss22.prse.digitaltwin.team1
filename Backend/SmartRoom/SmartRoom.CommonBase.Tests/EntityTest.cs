using SmartRoom.CommonBase.Core.Entities;
using System.Linq;
using Xunit;

namespace SmartRoom.CommonBase.Tests
{
    public class EntityTest
    {
        [Theory]
        [InlineData("Name")]
        [InlineData("EntityRefID")]
        [InlineData("TimeStamp")]
        [InlineData("Value")]
        [InlineData("Id")]
        public void BinaryState_PropertyNames_Exist(string name)
        { 
            Assert.NotNull(typeof(BinaryState).GetProperties().First(p => p.Name.Equals(name)));
        }

        [Theory]
        [InlineData("Name")]
        [InlineData("EntityRefID")]
        [InlineData("TimeStamp")]
        [InlineData("Value")]
        [InlineData("Id")]
        public void MeasureState_PropertyNames_Exist(string name)
        {
            Assert.NotNull(typeof(MeasureState).GetProperties().First(p => p.Name.Equals(name)));
        }
        //ToDo Check Propnames for all classes (Room, RoomEquipment)

        [Fact]
        public void MeasureState_EmtyCtor_ValidObject()
        {
            Assert.NotNull(new BinaryState());
        }
        //ToDo Check all emty ctors (measurestates, Room, RoomEquipment, State<object>)


    }
}
