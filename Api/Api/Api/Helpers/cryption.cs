using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Api.Helpers
{
    public class cryption
    {

        private const string password = "nyckel";

        public static string EncryptMessage(string plainMessage)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.IV = new byte[8];
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, new byte[0]);
            des.Key = pdb.CryptDeriveKey("RC2", "MD5", 128, new byte[8]);
            MemoryStream ms = new MemoryStream(plainMessage.Length * 2);
            CryptoStream encStream = new CryptoStream(ms, des.CreateEncryptor(),
            CryptoStreamMode.Write);
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainMessage);
            encStream.Write(plainBytes, 0, plainBytes.Length);
            encStream.FlushFinalBlock();
            byte[] encryptedBytes = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(encryptedBytes, 0, (int)ms.Length);
            encStream.Close();
            return Convert.ToBase64String(encryptedBytes);
        }

        public static string DecryptMessage(string encryptedBase64)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.IV = new byte[8];
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, new byte[0]);
            des.Key = pdb.CryptDeriveKey("RC2", "MD5", 128, new byte[8]);
            byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);
            MemoryStream ms = new MemoryStream(encryptedBase64.Length);
            CryptoStream decStream = new CryptoStream(ms, des.CreateDecryptor(),
            CryptoStreamMode.Write);
            decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            decStream.FlushFinalBlock();
            byte[] plainBytes = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(plainBytes, 0, (int)ms.Length);
            decStream.Close();
            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}
