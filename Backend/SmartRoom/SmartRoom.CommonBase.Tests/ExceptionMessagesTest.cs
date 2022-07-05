using SmartRoom.CommonBase.Core.Exceptions;
using Xunit;

namespace SmartRoom.CommonBase.Tests
{
    public class ExceptionMessagesTest
    {
        [Fact]
        public void UNEXPECTED_NotNullOrEmpty()
        {
            Assert.False(string.IsNullOrEmpty(Messages.UNEXPECTED));
        }

        [Fact]
        public void PARAMTERNULL_NotNullOrEmpty()
        {
            Assert.False(string.IsNullOrEmpty(Messages.PARAMTER_NULL));
        }
    }
}
