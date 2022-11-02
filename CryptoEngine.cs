using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winners_ITI
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    /// <summary>
    /// Third Wave Cryptor Engine for Encrypt Decript Process
    /// </summary>
    public class CryptoEngine
    {
        /// <summary>
        /// Decrypt string
        /// </summary>
        /// <param name="cipherString">String to Decrypt</param>
        /// <param name="key">Key string</param>
        /// <returns>String - Decrypted</returns>
        public static string Decrypt(string cipherString, string key)/**/
        {

            byte[] inputBuffer = Convert.FromBase64String(cipherString);
            SHA512CryptoServiceProvider provider = new SHA512CryptoServiceProvider();
            byte[] buffer = provider.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            byte[] trimmedBytes = new byte[24];
            Buffer.BlockCopy(buffer, 0, trimmedBytes, 0, 24);
            buffer = trimmedBytes;
            provider.Clear();
            TripleDESCryptoServiceProvider provider2 = new TripleDESCryptoServiceProvider();
            provider2.Mode = CipherMode.ECB;
            provider2.Padding = PaddingMode.PKCS7;
            provider2.Key = buffer;
            byte[] bytes = provider2.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            provider2.Clear();
            return Encoding.UTF8.GetString(bytes);
        }
        /// <summary>
        /// Encrypt String
        /// </summary>
        /// <param name="toEncrypt">String to Encrypt</param>
        /// <param name="key">Key string</param>
        /// <returns>String - Encrypted</returns>
        public static string Encrypt(string toEncrypt, string key)
        {
            byte[] bytes = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            SHA512CryptoServiceProvider provider = new SHA512CryptoServiceProvider();
            byte[] buffer = provider.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            byte[] trimmedBytes = new byte[24];
            Buffer.BlockCopy(buffer, 0, trimmedBytes, 0, 24);
            buffer = trimmedBytes;
            provider.Clear();
            TripleDESCryptoServiceProvider provider2 = new TripleDESCryptoServiceProvider
            {
                Key = buffer,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] inArray = provider2.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            provider2.Clear();
            return Convert.ToBase64String(inArray, 0, inArray.Length);

        }


        public static string Decrypt_MD5(string cipherString, string key)/**/
        {
            try
            {
                byte[] inputBuffer = Convert.FromBase64String(cipherString);
                MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                byte[] buffer = provider.ComputeHash(Encoding.UTF8.GetBytes(key));
                provider.Clear();
                TripleDESCryptoServiceProvider provider2 = new TripleDESCryptoServiceProvider
                {
                    Key = buffer,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                byte[] bytes = provider2.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                provider2.Clear();
                return Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Encrypt String
        /// </summary>
        /// <param name="toEncrypt">String to Encrypt</param>
        /// <param name="key">Key string</param>
        /// <returns>String - Encrypted</returns>
        public static string Encrypt_MD5(string toEncrypt, string key)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(toEncrypt);
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] buffer = provider.ComputeHash(Encoding.UTF8.GetBytes(key));
            provider.Clear();
            TripleDESCryptoServiceProvider provider2 = new TripleDESCryptoServiceProvider
            {
                Key = buffer,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] inArray = provider2.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            provider2.Clear();
            return Convert.ToBase64String(inArray, 0, inArray.Length);
        }
        public static string MigrateEncryptionToSHA512(string cypherString, string sKey)
        {
            try
            {
                string sDecryptedString = Decrypt_MD5(cypherString, sKey);
                string sNewlyEncryptedString = Encrypt(sDecryptedString, sKey);
                return sNewlyEncryptedString;
            }
            catch
            {
                throw;
            }
        }
    }
}
