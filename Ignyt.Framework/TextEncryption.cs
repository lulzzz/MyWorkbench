using System;
using System.Security.Cryptography;
using System.Text;

namespace Ignyt.Framework
{
    public static class TextEncryption
    {
        private static TripleDESCryptoServiceProvider _cryptDES3 = new TripleDESCryptoServiceProvider();
        private static MD5CryptoServiceProvider _cryptMD5Hash = new MD5CryptoServiceProvider();
        private static string key = "F827D671-787F-4350-AFA7-B4991E9F0C1C";

        public static string Encrypt(string Text)
        {
            if (Text == null)
                return Text;

            _cryptDES3.Key = _cryptMD5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
            _cryptDES3.Mode = CipherMode.ECB;
            ICryptoTransform desdencrypt = _cryptDES3.CreateEncryptor();
            byte[] buff = ASCIIEncoding.ASCII.GetBytes(Text);
            string Encrypt = Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));
            Encrypt = Encrypt.Replace("+", "!");
            return Encrypt;
        }

        public static string Decypt(string Text)
        {
            if (Text == null)
                return Text;

            Text = Text.Replace("!", "+");
            byte[] buf = new byte[Text.Length];
            _cryptDES3.Key = _cryptMD5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
            _cryptDES3.Mode = CipherMode.ECB;
            ICryptoTransform desdencrypt = _cryptDES3.CreateDecryptor();
            buf = Convert.FromBase64String(Text);
            string Decrypt = ASCIIEncoding.ASCII.GetString(desdencrypt.TransformFinalBlock(buf, 0, buf.Length));
            return Decrypt;
        }
    }
}
