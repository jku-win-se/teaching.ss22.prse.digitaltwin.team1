using SmartRoom.CommonBase.Tests.Models;
using SmartRoom.CommonBase.Utils;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartRoom.CommonBase.Tests
{
    public class GenericCSVWriterTest
    {
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
            using (var writer = new GenericCSVWriter<TestEntity>(new List<TestEntity> { new TestEntity { TestInt = 10 } }, "Test.csv"))
            {
                Assert.NotNull(writer.WriteToCSV());
            }
        }
    }
}
