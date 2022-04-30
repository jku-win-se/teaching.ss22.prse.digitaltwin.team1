using SmartRoom.CommonBase.Utils;
using System;
using Xunit;

namespace SmartRoom.CommonBase.Tests
{
    public class Aes256CipherTest
    {

        [Fact]
        public void Aes256CipherCtor_EmptyString_NullReferenceException()
        {
            Assert.Throws<NullReferenceException>( () => new Aes256Cipher(""));
           
        }

        [Fact]
        public void Decrypt_ValidEncString_ExpDecryptedString()
        {
            var cypher = new Aes256Cipher("sfShK7FHmK8kYU62EDhb3FhUQL4fXKhYINTYaeHjf6U=");
            var encrString = "XrOYnGAPkoTh4lB5zRdAAMWOEwZMgqD6kq7tXdI9JB5NhkL9khk/O6klzgBLLs9h";
            var expString = "1!$Sonderzeichen%";

            Assert.Equal(expString, cypher.Decrypt(encrString));    
        }
    }
}
