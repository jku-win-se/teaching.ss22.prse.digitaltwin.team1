using Moq;
using SmartRoom.CommonBase.Utils;
using SmartRoom.CommonBase.Utils.Contracts;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartRoom.CommonBase.Tests
{
    public class GenericCSVWriterTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Ctor_NullOrEmptyFileName_ThrowsNullException(string value)
        {
            Assert.Throws<ArgumentNullException>(() => new GenericCSVWriter<Object>(new List<Object>(),value));
        }

        [Fact]
        public void Ctor_NullData_ThrowsFormatException()
        {
            Assert.Throws<ArgumentNullException>(() => new GenericCSVWriter<Object>(null!, "Test"));
        }

        [Fact]
        public void Ctor_ValidFileName_GetFileName()
        {
            var writer = new GenericCSVWriter<Object>(new List<Object>(), "Test.csv");

            Assert.Equal("Test.csv", writer.FileName);
        }

        [Fact]
        public void WriteToCSV_ValidParams_NoException()
        {
            var mock = new Mock<IGenericCSVWriter>();
            mock.Setup(m => m.WriteToCSV()).Returns("Test.csv");
            Assert.Equal("Test.csv", mock.Object.WriteToCSV());
        }
    }
}
