﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SitradMovil.Models
{
    public class StringCrypto
    {
        private SymmetricAlgorithm mobjCryptoService;
        public enum SymmProvEnum : int
        {
            DES,
            RC2,
            Rijndael
        }

        public StringCrypto(SymmProvEnum NetSelected)
        {
            switch (NetSelected)
            {
                case SymmProvEnum.DES:
                    mobjCryptoService = new DESCryptoServiceProvider();
                    break;
                case SymmProvEnum.RC2:
                    mobjCryptoService = new RC2CryptoServiceProvider();
                    break;
                case SymmProvEnum.Rijndael:
                    mobjCryptoService = new RijndaelManaged();
                    break;
            }
        }

        public StringCrypto(SymmetricAlgorithm ServiceProvider)
        {
            mobjCryptoService = ServiceProvider;
        }

        private byte[] GetLegalKey(string Key)
        {
            string sTemp = null;
            if (mobjCryptoService.LegalKeySizes.Length > 0)
            {
                int lessSize = 0;
                int moreSize = mobjCryptoService.LegalKeySizes[0].MinSize;
                // key sizes are in bits
                while (Key.Length * 8 > moreSize)
                {
                    lessSize = moreSize;
                    moreSize += mobjCryptoService.LegalKeySizes[0].SkipSize;
                }
                sTemp = Key.PadRight(moreSize / 8, ' ');
            }
            else
            {
                sTemp = Key;
            }

            // convert the secret key to byte array
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        public string Encrypting(string Source, string Key)
        {
            byte[] bytIn = System.Text.ASCIIEncoding.ASCII.GetBytes(Source);
            // create a MemoryStream so that the process can be done without I/O files
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            byte[] bytKey = GetLegalKey(Key);

            // set the private key
            mobjCryptoService.Key = bytKey;
            mobjCryptoService.IV = bytKey;

            // create an Encryptor from the Provider Service instance
            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();

            // create Crypto Stream that transforms a stream using the encryption
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);

            // write out encrypted content into MemoryStream
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();

            // get the output and trim the '\0' bytes
            byte[] bytOut = ms.GetBuffer();
            int i = 0;
            for (i = 0; i <= bytOut.Length - 1; i++)
            {
                if (bytOut[i] == 0)
                {
                    break; // TODO: might not be correct. Was : Exit For
                }
            }

            // convert into Base64 so that the result can be used in xml
            return System.Convert.ToBase64String(bytOut, 0, i);
        }

        public string Decrypting(string Source, string Key)
        {
            // convert from Base64 to binary
            byte[] bytIn = System.Convert.FromBase64String(Source);
            // create a MemoryStream with the input
            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytIn, 0, bytIn.Length);

            byte[] bytKey = GetLegalKey(Key);

            // set the private key
            mobjCryptoService.Key = bytKey;
            mobjCryptoService.IV = bytKey;

            // create a Decryptor from the Provider Service instance
            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();

            // create Crypto Stream that transforms a stream using the decryption
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);

            // read out the result from the Crypto Stream
            System.IO.StreamReader sr = new System.IO.StreamReader(cs);
            return sr.ReadToEnd();
        }

    }
}