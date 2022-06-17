using SmartRoom.CommonBase.Tests.Models;
using SmartRoom.CommonBase.Utils;
using System;
using System.Collections.Generic;
using Xunit;


namespace SmartRoom.CommonBase.Tests
{
    public class GenericCSVReaderTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Ctor_NullOrEmptyString_ThrowsNullException(string value)
        {
            Assert.Throws<ArgumentNullException>(() => new GenericCSVReader<Object>(value));
        }

        [Fact]
        public void Ctor_NoCSVFileName_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => new GenericCSVReader<Object>("Test")); 
        }

        //Cant be Testet in Cercle CI
        //[Fact]
        //public void Read_ValidFile_Ok()
        //{
        //    using (var writer = new GenericCSVWriter<TestEntity>(new List<TestEntity> { new TestEntity { TestInt = 10 } }, "Test.csv"))
        //    using (var reader = new GenericCSVReader<TestEntity>("Test.csv"))
        //    {
        //        writer.WriteToCSV();
        //        Assert.NotEmpty(reader.Read());
        //    }
        //}
    }
}
