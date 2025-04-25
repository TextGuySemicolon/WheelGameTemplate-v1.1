using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;

public class EncryptionHandler : MonoBehaviour
{
    public static byte[] GetHashedKey(string privateKey)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(privateKey));
            Array.Resize(ref key, 32); // Ensure the key is 32 bytes long (you can resize to 16 or 24 as well)
            return key;
        }
    }

    public static string EncryptString(string plainText, string privateKey)
    {
        byte[] _hash = GetHashedKey(privateKey);

        byte[] key = _hash; // Encoding.UTF8.GetBytes(privateKey);
        byte[] iv = new byte[16]; // AES requires a 16-byte IV

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
    public static string DecryptString(string encryptedText, string privateKey)
    {
        byte[] _hash = GetHashedKey(privateKey);

        byte[] key = _hash;//Encoding.UTF8.GetBytes(privateKey);
        byte[] iv = new byte[16]; // AES requires a 16-byte IV
        byte[] buffer = Convert.FromBase64String(encryptedText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream(buffer))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }
}
