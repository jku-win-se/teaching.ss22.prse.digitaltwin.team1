using SmartRoom.CommonBase.Utils;
using System.Collections.Generic;
using Xunit;

namespace SmartRoom.CommonBase.Tests
{
    public class GenericMapperTest
    {
        [Fact]
        public void MapObject_ValidObjects_ValidResult() 
        {
            var objExp = new
            {
                TestString = "Test",
                TestBool = true,
                TestDouble = 10.21,
                TestInt = 2,
                TestList = new List<object>()
            };

            var objAct = new TestClass
            {
                TestString = "",
                TestBool = false,
                TestDouble = 1.1,
                TestInt = 1,
                TestList = new List<object>()
            };

            GenericMapper.MapObjects(objAct, objExp);

            Assert.Equal(objExp.TestString, objAct.TestString);
            Assert.Equal(objExp.TestBool, objAct.TestBool);
            Assert.Equal(objExp.TestDouble, objAct.TestDouble);
            Assert.Equal(objExp.TestInt, objAct.TestInt);
            Assert.Equal(objExp.TestList, objAct.TestList);
        }

        [Fact]
        public void MapObject_NullParams_DefaultResult()
        {
            TestClass objExp = null!;

            var res1 = GenericMapper.MapObjects(objExp, new { });
            var res2 = GenericMapper.MapObjects(new { }, objExp);

            Assert.Null(res1);
            Assert.Null(res2);
        }

        internal class TestClass
        {
            public string TestString { get; internal set; } = string.Empty;
            public bool TestBool { get; internal set; }
            public double TestDouble { get; internal set; }
            public int TestInt { get; internal set; }
            public List<object> TestList { get; internal set; } = new List<object>();
        }
    }
}
