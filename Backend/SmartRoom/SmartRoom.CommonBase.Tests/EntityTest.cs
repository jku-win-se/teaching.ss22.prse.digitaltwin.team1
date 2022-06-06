using SmartRoom.CommonBase.Core.Entities;
using System;
using System.Collections.Generic;
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
        [Fact]
        public void BinaryState_Properties_Ok()
        {
            var timeStamp = DateTime.Now;
            var entityRefId = new Guid("026bdf16-0d67-43ad-9d5d-afef430f1589");
            var name = "test";
            var id = new System.Guid("026bdf16-0d67-43ad-9d5d-afef430f1584");
            var value = true;

            var state = new BinaryState 
            {
                EntityRefID = entityRefId,
                Id = id,
                Name = name,
                TimeStamp = timeStamp,
                Value = value
            };

            Assert.Equal(timeStamp, state.TimeStamp);
            Assert.Equal(entityRefId, state.EntityRefID);
            Assert.Equal(name, state.Name);
            Assert.Equal(value, state.Value);
            Assert.Equal(id, state.Id);
           
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
       
        [Fact]
        public void MeasureState_Properties_Ok()
        {
            var timeStamp = DateTime.Now;
            var entityRefId = new Guid("026bdf16-0d67-43ad-9d5d-afef430f1589");
            var name = "test";
            var id = new System.Guid("026bdf16-0d67-43ad-9d5d-afef430f1584");
            var value = 10.03;

            var state = new MeasureState
            {
                EntityRefID = entityRefId,
                Id = id,
                Name = name,
                TimeStamp = timeStamp,
                Value = value
            };

            Assert.Equal(timeStamp, state.TimeStamp);
            Assert.Equal(entityRefId, state.EntityRefID);
            Assert.Equal(name, state.Name);
            Assert.Equal(value, state.Value);
            Assert.Equal(id, state.Id);

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
       
        [Fact]
        public void Room_Properties_Ok()
        {
            var name = "test";
            var id = new System.Guid("026bdf16-0d67-43ad-9d5d-afef430f1584");
            var building = "test";
            var peopleCount = 5;
            var roomEquipment = new List <RoomEquipment>();
            var roomType = "lab";
            var size = 20;


            var room = new Room
            {
                Id = id,
                Name = name,
                Building = building,
                PeopleCount = peopleCount,
                RoomEquipment = roomEquipment,
                RoomType = roomType,
                Size = size 
            };

            Assert.Equal(name, room.Name);
            Assert.Equal(id, room.Id);
            Assert.Equal(building, room.Building);
            Assert.Equal(peopleCount, room.PeopleCount);
            Assert.Equal(roomEquipment, room.RoomEquipment);
            Assert.Equal(roomType, room.RoomType);
            Assert.Equal(size, room.Size);
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
        public void RoomEquipment_Properties_Ok()
        {
            var roomId = new Guid("026bdf16-0d67-43ad-9d5d-afef430f1589");
            var name = "test";
            var id = new System.Guid("026bdf16-0d67-43ad-9d5d-afef430f1584");
            var equipmentRef = "test";
            
            var roomEquipment = new RoomEquipment
            {
                Id = id,
                Name = name,
                RoomID = roomId,
                EquipmentRef = equipmentRef
            };

            Assert.Equal(name, roomEquipment.Name);
            Assert.Equal(id, roomEquipment.Id);
            Assert.Equal(roomId, roomEquipment.RoomID);
            Assert.Equal(equipmentRef, roomEquipment.EquipmentRef);
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
    }
}
