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

        [Theory]
        [InlineData("Name")]
        [InlineData("PeopleCount")]
        [InlineData("Size")]
        [InlineData("RoomType")]
        [InlineData("Building")]
        [InlineData("RoomEquipment")]
        [InlineData("Id")]

        public void Room_PropertyNames_Exist(string name)
        {
            Assert.NotNull(typeof(Room).GetProperties().First(p => p.Name.Equals(name)));
        }

        [Theory]
        [InlineData("Name")]
        [InlineData("RoomID")]
        [InlineData("EquipmentRef")]
        [InlineData("Id")]

        public void RoomEquipment_PropertyNames_Exist(string name)
        {
            Assert.NotNull(typeof(RoomEquipment).GetProperties().First(p => p.Name.Equals(name)));
        }

        [Fact]
        public void BinaryState_EmtyCtor_ValidObject()
        {
            Assert.NotNull(new BinaryState());
        }

        [Fact]
        public void Room_EmtyCtor_ValidObject()
        {
            Assert.NotNull(new Room());
        }

        [Fact]
        public void MeasureState_EmtyCtor_ValidObject()
        {
            Assert.NotNull(new MeasureState());
        }

        [Fact]
        public void State_EmtyCtor_ValidObject()
        {
            Assert.NotNull(new State<object>());
        }
        //ToDo Check all emty ctors (State<object>)







    }
}
