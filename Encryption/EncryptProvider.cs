using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encryption
{
    static class EncryptProvider
    {
        public static string EncryptStringToBytes_Aes(string plainText)
        {

            string base64Key = "4/OJjBEZqAW9qr4E3MRFAVjQrcw4rQaCjV9tgbn4bdA=";
            string base64IV = "03zbj9oIGnLcJSyDBZ+8uQ==";

            byte[] Key = Convert.FromBase64String(base64Key);
            byte[] IV = Convert.FromBase64String(base64IV);

            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        public static string DecryptConnectionString()
        {
            string base64Key = "4/OJjBEZqAW9qr4E3MRFAVjQrcw4rQaCjV9tgbn4bdA=";
            string base64IV = "03zbj9oIGnLcJSyDBZ+8uQ==";

            byte[] Key = Convert.FromBase64String(base64Key);
            byte[] IV = Convert.FromBase64String(base64IV);

            string cnxnString = System.Configuration.ConfigurationManager.AppSettings["SecretConnection"];

            byte[] encryptedValueBytes = Convert.FromBase64String(cnxnString);

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(encryptedValueBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
