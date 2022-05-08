using SmartRoom.CommonBase.Tests.Models;
using SmartRoom.CommonBase.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        [Fact]
        public void Read_ValidCSVFile_ValidResult()
        {
            string path = "/root/project/Backend/SmartRoom/SmartRoom.CommonBase.Tests/bin/Release/net6.0";
            ICollection<CSVTestModel> result;
            using (GenericCSVReader<CSVTestModel> reader = new GenericCSVReader<CSVTestModel>(@$"{path}\Data\TestFile.csv"))
            {
                result = reader.Read().ToList();
            }

            Assert.Equal(19, result.Count);
            Assert.True(result.First().CompareTo(new CSVTestModel
            {
                Timestamp = Convert.ToDateTime("2022-02-15T08:00:00+0000"),
                Door_Id = 1,
                isOpen = true,
                Room_Id = "Room101",
                Temperature = 27.5
            })==0);
        }
    }
}
