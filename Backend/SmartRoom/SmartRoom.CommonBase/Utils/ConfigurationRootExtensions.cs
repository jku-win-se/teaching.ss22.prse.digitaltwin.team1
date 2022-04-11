using Microsoft.Extensions.Configuration;

namespace SmartRoom.CommonBase.Utils
{
    public static class ConfigurationRootExtensions
    {
        public static IConfigurationRoot Decrypt(this IConfigurationRoot root, string cipherPrefix)
        {
            var cipher = new Aes256Cipher("sfShK7FHmK8kYU62EDhb3FhUQL4fXKhYINTYaeHjf6U=");
            DecryptInChildren(root);
            return root;

            void DecryptInChildren(IConfiguration parent)
            {
                foreach (var child in parent.GetChildren())
                {
                    if (child.Value?.StartsWith(cipherPrefix) == true)
                    {
                        var cipherText = child.Value.Substring(cipherPrefix.Length);
                        parent[child.Key] = cipher.Decrypt(cipherText);
                    }

                    DecryptInChildren(child);
                }
            }
        }
    }
}
