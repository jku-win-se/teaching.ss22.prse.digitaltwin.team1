using SmartRoom.CommonBase.Utils;
using System;
using Xunit;

namespace SmartRoom.CommonBase.Tests
{
    public class Aes256CipherTest
    {
        private readonly string _key = "sfShK7FHmK8kYU62EDhb3FhUQL4fXKhYINTYaeHjf6U=";
        private readonly string _decrString = "1!$Sonderzeichen%";
        
        [Fact]
        public void Aes256CipherCtor_EmptyString_NullReferenceException()
        {
            Assert.Throws<ArgumentNullException>( () => new Aes256Cipher(""));
        }

        [Fact]
        public void Decrypt_ValidEncString_ExpDecryptedString()
        {
            var cypher = new Aes256Cipher(_key);
            var encrString = "XrOYnGAPkoTh4lB5zRdAAMWOEwZMgqD6kq7tXdI9JB5NhkL9khk/O6klzgBLLs9h";
          
            Assert.Equal(_decrString, cypher.Decrypt(encrString));    
        }
        [Fact]
        public void Encrypt_ValidDecrString_ExpEncryptedString()
        {
            var cypher = new Aes256Cipher(_key);
            var encrString = cypher.Encrypt(_decrString);
            Assert.Equal(_decrString, cypher.Decrypt(encrString));
        }
        [Fact]
        public void GenerateNewKey_Empty_ExpNewKey()
        {
            var val = Aes256Cipher.GenerateNewKey();
            Span<byte> buffer = new Span<byte>(new byte[val.Length]);
            Assert.True(Convert.TryFromBase64String(val, buffer, out int bytesParsed));
        }
    }
}
